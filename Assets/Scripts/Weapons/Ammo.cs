using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private int _maxAmmo = 200;

    private int _ammo;

    public bool IsEmpty => _ammo <= 0;
    
    private void Start()
    {
        SetAmmo(_maxAmmo);
    }

    private void SetAmmo(int ammo)
    {
        _ammo = ammo;
        
        Messenger<string>.Broadcast(GameEvents.SET_AMMO_TEXT, _ammo.ToString());
    }
    
    public void GetAmmo()
    {
        _ammo -= 1;

        Messenger<string>.Broadcast(GameEvents.SET_AMMO_TEXT, _ammo.ToString());
    }
}
