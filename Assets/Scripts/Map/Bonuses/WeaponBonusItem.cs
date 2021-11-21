using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponBonusItem : BonusItemCore
{
    [Header("References")] 
    [SerializeField] private Weapon _weapon;

    [Header("Preferences")]
    [SerializeField] private int _minAmmo = 40;
    [SerializeField] private int _maxAmmo = 100;
    
    [Header("Interact prefeernces")]
    [SerializeField] private float _swapDelay = 2f;

    private bool _canSwap;

    private void OnEnable()
    {
        _weapon.playerAmmo.SetAmmo(Random.Range(_minAmmo, _maxAmmo));
        
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