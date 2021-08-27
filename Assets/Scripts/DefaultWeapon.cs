using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DefaultWeapon : MonoBehaviour, IWeapon
{
    [Header("Prefabs")] [SerializeField] private GameObject _defaultBulletPrefab;
    [SerializeField] private Transform _bulletSpawnPlace;

    [Header("Gun options")] [SerializeField]
    private float _shootDelay = 0.1f;

    [Header("Animator")] [SerializeField] private Animator _animator;
    private static readonly int IsShooting = Animator.StringToHash("IsShooting");

    [SerializeField] private float _angleScatter = 1f;
    [SerializeField] private float _angleScatterOnSit = 2f;
    private float _previousAngleScatter;

    [SerializeField] private UnityEvent OnShoot;

    private Coroutine _shootingCoroutine;

    private void Awake()
    {
        _previousAngleScatter = _angleScatter;
        
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
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
        if (_shootingCoroutine != null) return;

        _animator.SetBool(IsShooting, true);

        _shootingCoroutine = StartCoroutine(Shoot());
    }

    public void StopShooting()
    {
        StopCoroutine(_shootingCoroutine);

        _animator.SetBool(IsShooting, false);

        _shootingCoroutine = null;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            OnShoot.Invoke();

            GameObject defaultBullet = Instantiate(_defaultBulletPrefab,
                _bulletSpawnPlace.position,
                Quaternion.Euler(0, 0, Random.Range(-_angleScatter, _angleScatter)));


            ChangeBulletDirection(ref defaultBullet, (int) transform.parent.parent.localScale.x);

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    //Changes bullet direction due to gun local scale
    private void ChangeBulletDirection(ref GameObject defaultBullet, int direction)
    {
        defaultBullet.transform.Rotate(0, 0, direction == 1 ? 0 : 180);
    }
}