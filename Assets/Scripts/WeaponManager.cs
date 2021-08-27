using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IWeapon
{
    [SerializeField] private Weapons _weapon;
    [SerializeField] private GameObject[] _weapons;

    [Header("Weapons")] 
    [SerializeField] private DefaultWeapon _defaultWeapon;
    
    //StartShootingDelegate
    private delegate void StartShootingDelegate();
    private StartShootingDelegate _startShootingDelegate;
    
    //StopShootingDelegate
    private delegate void StopShootingDelegate();
    private StopShootingDelegate _stopShootingDelegate;
    
    private void Start()
    {
        SetWeapon(_weapon);
    }

    public void SetWeapon(Weapons weapon)
    {
        SetActiveWeapon();
        
        switch (weapon)
        {
            case Weapons.DefaultWeapon:
            {
                SetDelagates(_defaultWeapon.StartShooting, _defaultWeapon.StopShooting);
                break;
            }
            default:
            {
                SetDelagates(_defaultWeapon.StartShooting, _defaultWeapon.StopShooting);
                break;
            }
        }
    }

    private void SetActiveWeapon()
    {
        foreach (var weapon in _weapons)
        {
            weapon.SetActive(false);
            
            if (weapon.name == _weapon.ToString())
            {
                weapon.SetActive(true);
            }
        }
    }
    
    private void SetDelagates(StartShootingDelegate startShootingDelegate, StopShootingDelegate stopShootingDelegate)
    {
        _startShootingDelegate = startShootingDelegate;
        _stopShootingDelegate = stopShootingDelegate;
    }
    
    public void StartShooting()
    {
        _startShootingDelegate();
    }

    public void StopShooting()
    {
        _stopShootingDelegate();
    }
}
