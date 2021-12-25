using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public static Action onShoot;

    protected bool _canShoot = true;

    private Tween _shootTween;

    protected void OnEnable()
    {
        _previousAngleScatter = _angleScatter;

        PlayerSitAndUpAnimation.onGetUp += OnPlayerGetUp;
        PlayerLegKickAnimation.onPlayed += StopShooting;
        PlayerSitAndUpAnimation.onSitDown += OnPlayerSitDown;
    }

    protected void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected void OnDisable()
    {
        PlayerSitAndUpAnimation.onGetUp -= OnPlayerGetUp;
        PlayerLegKickAnimation.onPlayed -= StopShooting;
        PlayerSitAndUpAnimation.onSitDown -= OnPlayerSitDown;
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
        if (_playerAmmo.IsEmpty)
        {
            _weaponVFX.PlayEmptyAmmoSound(_transform.position);

            return;
        }

        if (_shootCoroutine != null || CanShoot() == false) return;

        _shootCoroutine = StartCoroutine(Shoot());

        ControlShootSpeed();
    }

    protected void ControlShootSpeed()
    {
        _canShoot = false;
        _shootTween.Kill();
        _shootTween = this.DOWait(_shootDelay).OnComplete(() => { _canShoot = true; });
    }

    public void StopShooting()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);

            _shootCoroutine = null;

            ControlShootSpeed();
        }
    }

    protected virtual IEnumerator Shoot()
    {
        while (true)
        {
            if (CanShoot())
            {
                ShootActions();

                onShoot?.Invoke();
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
        return _canShoot && _playerAmmo.IsEmpty == false && PlayerLegKickAnimation.IsPlaying == false;
    }

    protected virtual void ShootActions()
    {
        _weaponVFX.PlayShootAudio(_audioSource);
        SpawnBullet();
        _playerAmmo.GetAmmo();
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletMuffSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.TriggerShootAnimation(_animator, ShootTrigger);
    }

    protected void SpawnBullet()
    {
        var bulletPosition = _bulletSpawnPlace.position;
        var bulletRotation = new Vector3(0, 0, Random.Range(-_angleScatter, _angleScatter));
        ChangeBulletDirection(ref bulletRotation);

        _objectPooler.GetFromPool(_bullet, bulletPosition, Quaternion.Euler(bulletRotation));
    }

    protected void ChangeBulletDirection(ref Vector3 rotation)
    {
        rotation += new Vector3(0, 0, PlayerMovement.Direction == 1 ? 0 : 180);
    }
}
