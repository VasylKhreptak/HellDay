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

    public static Action<float> onImpactMovement;
    
    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        
        SetWeapon(_startupWeapon);
    }

    public void SetWeapon(Weapons weaponToSelect)
    {
        foreach (var weapon in _weapons)
        {
            weapon.gameObject.SetActive(weapon.weaponType == weaponToSelect);

            if (weapon.weaponType == weaponToSelect)
            {
                currentWeapon = weapon;
                ImpactPlayerMovement(weapon.MovementImpact);
            }
        }
    }

    public void SwapWeapon(Weapon toWeapon)
    {
        ThrowCurrentWeapon();
        
        SetWeapon(toWeapon.weaponType);
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
        onImpactMovement?.Invoke(percentage);
    }

    public void StartShooting()
    {
        if (CanShoot() == false) return;

        currentWeapon.IWeapon.StartShooting(); 
    }

    public void StopShooting()
    {
        if (CanShoot() == false) return;
        
        currentWeapon.IWeapon.StopShooting();
    }

    private bool CanShoot()
    {
        return gameObject.transform.parent.gameObject.activeSelf;
    }

}