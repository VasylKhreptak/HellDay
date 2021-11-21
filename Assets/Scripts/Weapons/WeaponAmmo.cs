using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected int _startupAmmo = 200;
    protected int _ammo;

    public bool IsEmpty => _ammo <= 0;
    public int Ammo => _ammo;
    
    protected virtual void Awake()  
    {
        SetAmmo(_startupAmmo);
    }

    public void SetAmmo(int ammo)
    {
        _ammo = ammo;
    }
    
    public virtual void GetAmmo()
    {
        _ammo -= 1;
    }
}
