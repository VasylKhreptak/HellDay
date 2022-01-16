using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WeaponBonusItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponBonusItemData _data;

    [Header("References")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private OnCollisionWithPlayerEvent _onCollisionWithPlayerEvent;

    [Header("Preferences")]
    [SerializeField] private int _minAmmo = 40;
    [SerializeField] private int _maxAmmo = 100;

    public static Action onTook;

    private bool _canSwap;

    private PlayerWeaponControl _playerWeaponControl;

    private Tween _waitTween;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnEnable()
    {
        ConfigureSwapSpeed();
        
        _weapon.playerAmmo.SetAmmo(Random.Range(_minAmmo, _maxAmmo));

        _onCollisionWithPlayerEvent.onCollision += ProcessCollisionWithPlayer;
    }

    private void OnDisable()
    {
        _waitTween.Kill();
        
        _onCollisionWithPlayerEvent.onCollision -= ProcessCollisionWithPlayer;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReloadReferences();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        ReloadReferences();
    }

    private void ReloadReferences()
    {
        _playerWeaponControl = GameAssets.Instance.playerWeaponControl;
    }
    
    private void ProcessCollisionWithPlayer(Collision2D other)
    {
        if (_canSwap == false ) return;

        if (other.gameObject.activeSelf)
        {
            onTook?.Invoke();

            _playerWeaponControl.SwapWeapon(_weapon);

            ConfigureSwapSpeed();

            gameObject.SetActive(false);
        }
    }

    private void ConfigureSwapSpeed()
    {
        _canSwap = false;
        _waitTween.Kill();
        _waitTween = this.DOWait(_data.SwapDelay).OnComplete(() => { _canSwap = true; });
    }
}