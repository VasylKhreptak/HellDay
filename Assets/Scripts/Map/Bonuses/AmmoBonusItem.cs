using UnityEngine;
using Random = UnityEngine.Random;

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

    private PlayerWeaponControl _playerWeaponControl;

    private void Start()
    {
        _playerWeaponControl = PlayerWeaponControl.Instance;
    }

    protected override void OnCollisionWithPlayer(Collision2D player)
    {
        Transform weaponControlTransform = player.transform.GetChild(0);
        
        if (weaponControlTransform != null)
        {
            AssignAmmo(_playerWeaponControl.currentWeapon);
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
