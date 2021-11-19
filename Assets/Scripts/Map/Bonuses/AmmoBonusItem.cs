using UnityEngine;

public class AmmoBonusItem : BonusItemCore
{
    [Header("Preferences")]
    [SerializeField] private int _minAmmo = 20;
    [SerializeField] private int _maxAmmo = 60;
    
    protected override void OnCollisionWithPlayer(Collision2D player)
    {
        if (player.transform.GetChild(0).TryGetComponent(out PlayerWeaponControl weaponControl) && 
            weaponControl.transform.parent.gameObject.activeSelf)
        {
            PlayerAmmo playerAmmo = weaponControl.currentWeapon.playerAmmo;
            
           playerAmmo.SetAmmoWithTextUpdate(playerAmmo.Ammo + Random.Range(_minAmmo, _maxAmmo));
        }

        gameObject.SetActive(false);
    }
}
