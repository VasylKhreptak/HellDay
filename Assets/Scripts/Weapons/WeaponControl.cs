using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    [SerializeField] private Weapons _weaponToSelect;
    [SerializeField] private Weapon[] _weapons;
    
    public static readonly float defaultBulletDamage = 10;

    private Weapon _currentWeapon;

    private void Start()
    {
        SetWeapon(_weaponToSelect);
    }

    public void SetWeapon(Weapons weaponToSelect)
    {
        foreach (var weapon in _weapons)
        {
            weapon.gameObject.SetActive(weapon.WeaponType == weaponToSelect);

            if (weapon.WeaponType == weaponToSelect)
            {
                _currentWeapon = weapon;
                ImpactPlayerMovement(weapon.MovementImpact);
            }
        }
    }

    private void ImpactPlayerMovement(float percentage)
    {
        Messenger<float>.Broadcast(GameEvents.PLAYER_MOVEMENT_IMPACT, percentage);
    }

    public void StartShooting()
    {
        if (CanShoot() == false) return;

        _currentWeapon.Iweapon.StartShooting(); 
    }

    public void StopShooting()
    {
        if (CanShoot() == false) return;
        
        _currentWeapon.Iweapon.StopShooting();
    }

    private bool CanShoot()
    {
        return gameObject.transform.parent.gameObject.activeSelf;
    }

}