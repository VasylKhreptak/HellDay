using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AmmoBonusItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnCollisionWithPlayerEvent _onCollisionWithPlayerEvent;
    
    [Header("Data")]
    [SerializeField] private AmmoBonusItemData _data;

    public static Action onApply;

    private PlayerWeaponControl _playerWeaponControl;

    private bool _canApply = true;

    private Tween _waitTween;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnEnable()
    {
        _onCollisionWithPlayerEvent.onCollision += ProcessCollisionWithPlayer;
    }

    private void OnDisable()
    {
        _onCollisionWithPlayerEvent.onCollision -= ProcessCollisionWithPlayer;
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
        if (_playerWeaponControl.transform.parent.gameObject.activeSelf == false)
            return;

        if (_canApply == false)
            return;

        onApply?.Invoke();
        
        AssignAmmo(_playerWeaponControl.currentWeapon);

        ControlApplySpeed();
        
        gameObject.SetActive(false);
    }

    private void ControlApplySpeed()
    {
        _canApply = false;
        _waitTween.Kill();
        _waitTween = this.DOWait(_data.ApplyDelay).OnComplete(() => { _canApply = true; });
    }

    private void AssignAmmo(Weapon currentWeapon)
    {
        foreach (var preference in _data.bonusPreferences)
            if (currentWeapon.weaponType == preference.weaponType)
                currentWeapon.playerAmmo.SetAmmoWithTextUpdate(currentWeapon.playerAmmo.Ammo +
                                                               Random.Range(preference.minAmmo, preference.maxAmmo));
    }
}