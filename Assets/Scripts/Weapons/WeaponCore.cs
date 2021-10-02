using System.Collections;
using UnityEngine;

public class WeaponCore : MonoBehaviour, IWeapon
{
    [Header("Positions")]
    [SerializeField] protected Transform _bulletMuffSpawnPlace;
    [SerializeField] protected Transform _bulletSpawnPlace;
    
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

    [Header("Player movement impact")]
    [ Tooltip("the percentage that will reduce staff mobility")]
    [SerializeField, Range(0, 70)] private float _movementImpact = 10f;

    protected Coroutine _shootingCoroutine;
    protected ObjectPooler _objectPooler;

    protected void Awake()
    {
        _previousAngleScatter = _angleScatter;

        //PlayerMovement
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.AddListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    protected void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected void OnEnable()
    {
        Messenger<float>.Broadcast(GameEvent.PLAYER_MOVEMENT_IMPACT, _movementImpact);
        
        SetAmmo(_maxAmmo);
    }

    protected void SetAmmo(int ammo)
    {
        _ammo = ammo;
        
        Messenger<string>.Broadcast(GameEvent.SET_AMMO_TEXT, _ammo.ToString());
    }

    protected void OnDestroy()
    {
        //PlayerMovement
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    protected void OnLegPunched(float time)
    {
        if (gameObject.activeSelf == false)
        {
            return;
        }

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
        if (_shootingCoroutine != null || _canShoot == false) return;

        _shootingCoroutine = StartCoroutine(Shoot());
        
        StartCoroutine(ControlShootSpeed());
    }

    public void StopShooting()
    {
        if (_shootingCoroutine != null)
            StopCoroutine(_shootingCoroutine);
        
        _shootingCoroutine = null;
    }

    protected virtual IEnumerator Shoot()
    {
        while (true)
        {
            if (_canShoot == true)
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
        
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletSpawnPlace.position, Quaternion.identity);
        
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _bulletSpawnPlace.position, Quaternion.identity);
        
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _bulletSpawnPlace.position, Quaternion.identity);
        
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
    

    //Changes bullet direction due to gun local scale
    protected void ChangeBulletDirection(ref Vector3 rotation)
    {
        rotation += new Vector3(0, 0, PlayerMovement.movementDirection == 1 ? 0 : 180);
    }
}