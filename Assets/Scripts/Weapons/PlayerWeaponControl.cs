using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private Weapon[] _weapons;
    
    [Header("Preferences")]
    [SerializeField] private Weapons _weaponToSelect;
    
    public static readonly float defaultBulletDamage = 10;
    [HideInInspector] public Weapon currentWeapon;

    private ObjectPooler _objectPooler;
    
    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        
        SetWeapon(_weaponToSelect);
    }

    public void SetWeapon(Weapons weaponToSelect)
    {
        foreach (var weapon in _weapons)
        {
            weapon.gameObject.SetActive(weapon.WeaponType == weaponToSelect);

            if (weapon.WeaponType == weaponToSelect)
            {
                currentWeapon = weapon;
                ImpactPlayerMovement(weapon.MovementImpact);
            }
        }
    }

    public void SwapWeapon(Weapon toWeapon)
    {
        ThrowCurrentWeapon();
        
        SetWeapon(toWeapon.WeaponType);
        currentWeapon.playerAmmo.SetAmmoWithTextUpdate(toWeapon.playerAmmo.Ammo);
    }

    private void ThrowCurrentWeapon()
    {
        GameObject weaponObj = _objectPooler.GetFromPool(currentWeapon.weaponPoolName,
            _transform.position, Quaternion.identity);

        weaponObj.transform.localScale = _transform.localScale;

        if (weaponObj.TryGetComponent(out PlayerAmmo playerAmmo))
        {
            playerAmmo.SetAmmo(currentWeapon.playerAmmo.Ammo);
        }
    }

    private void ImpactPlayerMovement(float percentage)
    {
        Messenger<float>.Broadcast(GameEvents.PLAYER_MOVEMENT_IMPACT, percentage);
    }

    public void StartShooting()
    {
        if (CanShoot() == false) return;

        currentWeapon.Iweapon.StartShooting(); 
    }

    public void StopShooting()
    {
        if (CanShoot() == false) return;
        
        currentWeapon.Iweapon.StopShooting();
    }

    private bool CanShoot()
    {
        return gameObject.transform.parent.gameObject.activeSelf;
    }

}