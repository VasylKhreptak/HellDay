using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponBonusItem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponBonusItemData _data;

    [Header("References")]
    [SerializeField] private Weapon _weapon;

    [Header("Preferences")]
    [SerializeField] private int _minAmmo = 40;
    [SerializeField] private int _maxAmmo = 100;

    private bool _canSwap;

    private PlayerWeaponControl _playerWeaponControl;

    private Tween _waitTween;

    private void Start()
    {
        _playerWeaponControl = GameAssets.Instance.playerWeaponControl;
    }

    private void OnEnable()
    {
        _weapon.playerAmmo.SetAmmo(Random.Range(_minAmmo, _maxAmmo));

        ConfigureSwapSpeed();
    }

    private void OnCollisionEnter2D(Collision2D player)
    {
        if (_data.playerLayerMask.ContainsLayer(player.gameObject.layer) == false ||
            _canSwap == false) return;

        if (player.gameObject.activeSelf)
        {
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

    private void OnDisable()
    {
        _waitTween.Kill();
    }
}
