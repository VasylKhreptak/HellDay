using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected int _maxAmmo = 200;

    protected int _ammo;

    public bool IsEmpty => _ammo <= 0;
    
    protected void Start()  
    {
        SetAmmo(_maxAmmo);
    }

    protected virtual void SetAmmo(int ammo)
    {
        _ammo = ammo;
    }
    
    public virtual void GetAmmo()
    {
        _ammo -= 1;
    }
}
