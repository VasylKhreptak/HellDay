using DG.Tweening;
using UnityEngine;

public class WeaponBonusItem : BonusItemCore
{
    [Header("References")] 
    [SerializeField] private Weapon _weapon;

    [Header("Startup Preferences")]
    [SerializeField] private int _minAmmo = 40;
    [SerializeField] private int _maxAmmo = 120;

    [Header("Interact prefeernces")]
    [SerializeField] private float _swapDelay = 2f;

    private bool _canSwap;

    private void Start()
    {
        // _weapon.playerAmmo.SetAmmo(Random.Range(_minAmmo, _maxAmmo));
        _weapon.playerAmmo.SetAmmo(123);
    }

    private void OnEnable()
    {
        ConfigureSwapSpeed();
    }

    protected override void OnCollisionWithPlayer(Collision2D player)
    {
        if (_canSwap == false) return;

        if (player.transform.GetChild(0).gameObject.TryGetComponent(out PlayerWeaponControl playerWeaponControl) &&
            player.gameObject.activeSelf)
        {
           playerWeaponControl.SwapWeapon(_weapon);
           
           ConfigureSwapSpeed();

           gameObject.SetActive(false);
        }
    }

    private void ConfigureSwapSpeed()
    {
        _canSwap = false;
        this.DOWait(_swapDelay).OnComplete(() => { _canSwap = true; });
    }

    private void OnDisable()
    {
        this.DOKill();
    }
}