using UnityEngine;

public class AmmoBonusItem : BonusItemCore
{
    [System.Serializable]
    private class BonusPreference
    {
        public Weapons weaponType;
        public int minAmmo;
        public int maxAmmo;
    }

    [Header("Bonus preferences")] 
    [SerializeField] private BonusPreference[] _bonusPreferences;
    
    protected override void OnCollisionWithPlayer(Collision2D player)
    {
        if (player.transform.GetChild(0).TryGetComponent(out PlayerWeaponControl weaponControl) && 
            weaponControl.transform.parent.gameObject.activeSelf)
        {
            AssignAmmo(weaponControl.currentWeapon);
        }

        gameObject.SetActive(false);
    }

    private void AssignAmmo(Weapon currentWeapon)
    {
        foreach (var preference in _bonusPreferences)
        {
            if (currentWeapon.WeaponType == preference.weaponType)
            {
                currentWeapon.playerAmmo.SetAmmoWithTextUpdate(currentWeapon.playerAmmo.Ammo + 
                                                               Random.Range(preference.minAmmo, preference.maxAmmo));
            }
        }
    }
}
