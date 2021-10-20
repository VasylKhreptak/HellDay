using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WeaponCore : MonoBehaviour, IWeapon
{
    [Header("References")] 
    [SerializeField] protected Transform _transform;
    
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
    [SerializeField] protected int _maxAmmo = 100;
    protected int _ammo;

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

        //PlayerMovement
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger<float>.AddListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);

        _transform.DOWait(0.2f, () => { SetAmmo(_maxAmmo); });
    }

    protected void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected void SetAmmo(int ammo)
    {
        _ammo = ammo;
        
        Messenger<string>.Broadcast(GameEvent.SET_AMMO_TEXT, _ammo.ToString());
    }

    protected void OnDisable()
    {
        //PlayerMovement
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    protected void OnLegPunched(float time)
    {
        StartCoroutine(OnLegPunchedCoroutine(time));
    }

    protected IEnumerator OnLegPunchedCoroutine(float animationDuration)
    {
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

        _shootCoroutine = StartCoroutine(Shoot());
        
        StartCoroutine(ControlShootSpeed());
    }

    public void StopShooting()
    {
        if (_shootCoroutine != null)
            StopCoroutine(_shootCoroutine);
        
        _shootCoroutine = null;
    }

    protected virtual IEnumerator Shoot()
    {
        while (true)
        {
            if (_canShoot)
            {
                ShootActions();
            }

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    protected virtual void ShootActions()
    {
        _weaponVFX.PlayShootAudio(_audioSource);
        SpawnBullet();
        GetAmmo();
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletMuffSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.StartShootAnimation(_animator, ShootTrigger);
    }

    protected IEnumerator ControlShootSpeed()
    {
        _canShoot = false;

        yield return new WaitForSeconds(_shootDelay);

        _canShoot = true;
    }

    protected void SpawnBullet()
    {
        Vector3 bulletPosition = _bulletSpawnPlace.position;
        Vector3 bulletRotation = new Vector3(0, 0, Random.Range(-_angleScatter, _angleScatter));
        ChangeBulletDirection(ref bulletRotation);

        _objectPooler.GetFromPool(_bullet, bulletPosition, Quaternion.Euler(bulletRotation));
    }

    protected void GetAmmo()
    {
        _ammo -= 1;

        Messenger<string>.Broadcast(GameEvent.SET_AMMO_TEXT, _ammo.ToString());

        if (_ammo <= 0)
            _canShoot = false;
    }
    
    protected void ChangeBulletDirection(ref Vector3 rotation)
    {
        rotation += new Vector3(0, 0, PlayerMovement.MovementDirection == 1 ? 0 : 180);
    }
}