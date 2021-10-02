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
            weapon.gameObject.SetActive(weapon.weaponType == weaponToSelect);

            if (weapon.weaponType == weaponToSelect)
            {
                _currentWeapon = weapon;
            }
        }
    }

    public void StartShooting()
    {
        if (CanShoot() == false)
        {
            return;
        }
        
        _currentWeapon.iWeapon.StartShooting();
        
        Messenger.Broadcast(GameEvent.PLAYED_AUDIO_SOURCE, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    public void StopShooting()
    {
        if (CanShoot() == false)
        {
            return;
        }
        
        _currentWeapon.iWeapon.StopShooting();
    }

    private bool CanShoot()
    {
        return gameObject.transform.parent.gameObject.activeSelf == true;
    }

}