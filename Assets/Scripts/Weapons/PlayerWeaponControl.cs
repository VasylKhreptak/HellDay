using System;
using Unity.Collections;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private Weapon[] _weapons;
    
    [Header("Preferences")]
    [SerializeField] private Weapons _startupWeapon;
    
    [HideInInspector] public Weapon currentWeapon;

    private ObjectPooler _objectPooler;

    public static PlayerWeaponControl Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        
        SetWeapon(_startupWeapon);
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