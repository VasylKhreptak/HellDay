using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class DefaultWeapon : MonoBehaviour, IWeapon
{
    [Header("Prefabs")]
    [SerializeField] private Transform _bulletSpawnPlace;

    [Header("Gun options")] [SerializeField]
    private float _shootDelay = 0.1f;

    [SerializeField] private bool _canShoot = true;

    [Header("Animator")] [SerializeField] private Animator _animator;
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");

    [SerializeField] private float _angleScatter = 1f;
    [SerializeField] private float _angleScatterOnSit = 2f;
    private float _previousAngleScatter;

    [SerializeField] private UnityEvent OnShoot;

    private Coroutine _shootingCoroutine;

    private ObjectPooler _objectPooler;

    private void Awake()
    {
        _previousAngleScatter = _angleScatter;

        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.AddListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    private void OnLegPunched(float animationDuration)
    {
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
        if (!_canShoot) return;

        if (_shootingCoroutine != null) return;

        _animator.SetBool(IsShooting, true);

        _shootingCoroutine = StartCoroutine(Shoot());
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

            GameObject defaultBullet = _objectPooler.GetFromPool(Pools.DefaultBullet);

            defaultBullet.transform.position = _bulletSpawnPlace.position;
            defaultBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_angleScatter, _angleScatter));

            ChangeBulletDirection(ref defaultBullet, (int) transform.parent.parent.localScale.x);
            
            Messenger.Broadcast(GameEvent.OBJECT_SPAWNED);

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    //Changes bullet direction due to gun local scale
    private void ChangeBulletDirection(ref GameObject defaultBullet, int direction)
    {
        defaultBullet.transform.Rotate(0, 0, direction == 1 ? 0 : 180);
    }
}