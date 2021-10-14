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
        for (var i = 0; i < _weapons.Length; i++)
        {
            var weapon = _weapons[i];
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
        Messenger<float>.Broadcast(GameEvent.PLAYER_MOVEMENT_IMPACT, percentage);
    }

    public void StartShooting()
    {
        if (CanShoot() == false)
        {
            return;
        }
        
        _currentWeapon.Iweapon.StartShooting();
        
        Messenger.Broadcast(GameEvent.PLAYED_AUDIO_SOURCE, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    public void StopShooting()
    {
        if (CanShoot() == false)
        {
            return;
        }
        
        _currentWeapon.Iweapon.StopShooting();
    }

    private bool CanShoot()
    {
        return gameObject.transform.parent.gameObject.activeSelf == true;
    }

}