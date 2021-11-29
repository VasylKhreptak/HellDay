using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WeaponCore : MonoBehaviour, IWeapon
{
    [Header("References")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected PlayerAmmo _playerAmmo;
    
    [Header("Positions")]
    [SerializeField] protected Transform _bulletMuffSpawnPlace;
    [SerializeField] protected Transform _bulletSpawnPlace;
    [SerializeField] protected Transform _shootParticleSpawnPlace;
    
    [Header("Weapon options")] 
    [Tooltip("Time before two shoots")] 
    [SerializeField] protected float _shootDelay = 0.1f;
    [SerializeField] protected float _angleScatter = 5f; 
    [SerializeField] protected bool _canShoot = true;

    [Header("Animator")] 
    [SerializeField] protected Animator _animator;
    protected static readonly int ShootTrigger = Animator.StringToHash("Shoot");
    
    [Header("Ammo options")] 
    [SerializeField] protected Pools _bullet = Pools.DefaultBullet;
    [SerializeField] protected Pools _bulletMuff = Pools.DefaultBulletMuff;
    
    [Header("Audio")] 
    [SerializeField] protected AudioSource _audioSource;

    [Header("WeaponVFX")] 
    [SerializeField] protected WeaponVFX _weaponVFX;
    
    [Header("Weapon option on condition")] 
    [SerializeField] protected float _angleScatterOnSit = 2f;
    protected float _previousAngleScatter;

    protected Coroutine _shootCoroutine;
    protected ObjectPooler _objectPooler;
    
    
    protected void OnEnable()
    {
        _previousAngleScatter = _angleScatter;

        Messenger.AddListener(GameEvents.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger<float>.AddListener(GameEvents.PLAYER_LEG_KICK, OnLegPunched);
        Messenger.AddListener(GameEvents.PLAYER_SIT_DOWN, OnPlayerSitDown);
    }

    protected void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected void OnDisable()
    {
        Messenger.RemoveListener(GameEvents.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvents.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvents.PLAYER_LEG_KICK, OnLegPunched);
    }

    protected virtual void OnLegPunched(float time)
    {
        StartCoroutine(OnLegPunchedCoroutine(time));
    }

    protected IEnumerator OnLegPunchedCoroutine(float animationDuration)
    {
        StopShooting();
        
        _canShoot = false;

        yield return new WaitForSeconds(animationDuration);

        _canShoot = true;
    }

    protected void OnPlayerGetUp()
    {
        _angleScatter = _previousAngleScatter;
    }

    protected void OnPlayerSitDown()
    {
        _angleScatter = _angleScatterOnSit;
    }

    public void StartShooting()
    {
        if (_shootCoroutine != null || _canShoot == false) return;
            
        if (_playerAmmo.IsEmpty) _weaponVFX.PlayEmptyAmmoSound(_transform.position);
        
        _shootCoroutine = StartCoroutine(Shoot());
        
        ControlShootSpeed();
    }

    protected void ControlShootSpeed()
    {
        _canShoot = false;
        this.DOWait(_shootDelay).OnComplete(() => { _canShoot = true; });
    }

    public void StopShooting()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
        
        _shootCoroutine = null;
    }

    protected virtual IEnumerator Shoot()
    {
        Messenger.Broadcast(GameEvents.PLAYED_LOUD_AUDIO_SOURCE, MessengerMode.DONT_REQUIRE_LISTENER);

        while (true)
        {
            if (CanShoot())
            {
                ShootActions();
            }
            else
            {
                StopShooting();
            }

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    protected bool CanShoot()
    {
        return _canShoot && _playerAmmo.IsEmpty == false;
    }

    protected virtual void ShootActions()
    {
        _weaponVFX.PlayShootAudio(_audioSource);
        SpawnBullet();
        _playerAmmo.GetAmmo();
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletMuffSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.StartShootAnimation(_animator, ShootTrigger);
    }

    protected void SpawnBullet()
    {
        Vector3 bulletPosition = _bulletSpawnPlace.position;
        Vector3 bulletRotation = new Vector3(0, 0, Random.Range(-_angleScatter, _angleScatter));
        ChangeBulletDirection(ref bulletRotation);

        _objectPooler.GetFromPool(_bullet, bulletPosition, Quaternion.Euler(bulletRotation));
    }

    protected void ChangeBulletDirection(ref Vector3 rotation)
    {
        rotation += new Vector3(0, 0, PlayerMovement.MovementDirection == 1 ? 0 : 180);
    }
}