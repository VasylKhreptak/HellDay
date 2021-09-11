using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour, IWeapon
{
    [Header("Weapon type")]
    [SerializeField] private Weapons _weaponType;
    
    [Header("Positions")]
    [SerializeField] private Transform _bulletSpawnPlace;
    [SerializeField] private Transform _bulletMuffSpawnPlace;
    
    [Header("Weapon options")] 
    [Tooltip("Time before two shoots")]
    [SerializeField] private float _shootDelay = 0.1f;
    [SerializeField] private float _angleScatter = 5f;
    [SerializeField] private bool _canShoot = true;
    [Tooltip("Number of balls in shotgun weapon")]
    [SerializeField] private int _shotGunCaliber = 10;

    [Header("Ammo options")]
    [SerializeField] private Pools _bullet = Pools.DefaultBullet;
    [SerializeField] private Pools _bulletMuff = Pools.DefaultBulletMuff;

    [Header("Animator")] 
    [SerializeField] private Animator _animator;
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");

    [Header("Weapon option on condition")]
    [SerializeField] private float _angleScatterOnSit = 2f;
    private float _previousAngleScatter;

    [Header("OnShootEvent")]
    [SerializeField] private UnityEvent OnShoot;

    private Coroutine _shootingCoroutine;
    private ObjectPooler _objectPooler;

    private void Awake()
    {
        _previousAngleScatter = _angleScatter;

        //PlayerMovement
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.AddListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
        
        //Weapnos
        Messenger<Weapons>.AddListener(GameEvent.SET_WEAPON, CheckWeaponConformity);
        Messenger.AddListener(GameEvent.START_SHOOTING, StartShooting);
        Messenger.AddListener(GameEvent.STOP_SHOOTING, StopShooting);
    }

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnDestroy()
    {
        //PlayerMovement
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
        
        //Weapnos
        Messenger<Weapons>.RemoveListener(GameEvent.SET_WEAPON, CheckWeaponConformity);
        Messenger.RemoveListener(GameEvent.START_SHOOTING, StartShooting);
        Messenger.RemoveListener(GameEvent.STOP_SHOOTING, StopShooting);
    }

    private void CheckWeaponConformity(Weapons weapon)
    {
        if (weapon != _weaponType)
            gameObject.SetActive(false);
    }
    
    private void OnLegPunched(float animationDuration)
    {
        if(!gameObject.activeSelf) return;
        
        StartCoroutine(OnLegPunchedCoroutine(animationDuration));
    }

    private IEnumerator OnLegPunchedCoroutine(float animationDuration)
    {
        _canShoot = false;

        yield return new WaitForSeconds(animationDuration);

        _canShoot = true;
    }

    private void OnPlayerGetUp()
    {
        _angleScatter = _previousAngleScatter;
    }

    private void OnPlayerSitDown()
    {
        _angleScatter = _angleScatterOnSit;
    }

    public void StartShooting()
    {
        if (_shootingCoroutine != null || !_canShoot || !gameObject.activeSelf) return;

        _shootingCoroutine = StartCoroutine(Shoot());

        StartCoroutine(ControlShootSpeed());
    }

    public void StopShooting()
    {
        if (_shootingCoroutine != null)
            StopCoroutine(_shootingCoroutine);

        _animator.SetBool(IsShooting, false);

        _shootingCoroutine = null;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            OnShoot.Invoke();

            if (_weaponType == Weapons.Shotgun)
            {
                for (int i = 0; i < _shotGunCaliber; i++)
                {
                    SpawnBullet();
                }
            }
            else
            {
                SpawnBullet();
            }

            SpawnBulletMuff();
            
            _animator.SetTrigger(IsShooting);

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    private IEnumerator ControlShootSpeed()
    {
        _canShoot = false;

        yield return new WaitForSeconds(_shootDelay);

        _canShoot = true;
    }
    
    private void SpawnBullet()
    {
        GameObject defaultBullet = _objectPooler.GetFromPool(_bullet);

        defaultBullet.transform.position = _bulletSpawnPlace.position;
        defaultBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_angleScatter, _angleScatter));

        ChangeBulletDirection(ref defaultBullet, (int) transform.parent.parent.localScale.x);
        
        Messenger<Pools, GameObject>.Broadcast(GameEvent.POOL_OBJECT_SPAWNED, Pools.DefaultBullet, defaultBullet);
    }

    private void SpawnBulletMuff()
    {
        GameObject defaultBulletMuff = _objectPooler.GetFromPool(_bulletMuff);

        defaultBulletMuff.transform.position = _bulletMuffSpawnPlace.position;
        
        Messenger<Pools, GameObject>.Broadcast(GameEvent.POOL_OBJECT_SPAWNED, Pools.DefaultBulletMuff, defaultBulletMuff);
    }
    
    //Changes bullet direction due to gun local scale
    private void ChangeBulletDirection(ref GameObject defaultBullet, int direction)
    {
        defaultBullet.transform.Rotate(0, 0, direction == 1 ? 0 : 180);
    }
}