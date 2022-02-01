using System;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class OnTakeDamageVibration : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Preferences")]
    [SerializeField] private HapticTypes _vibrationType = HapticTypes.LightImpact;
    
    [Header("PlayerPrefs Preferences")]
    [SerializeField] private string _playerPrefsKey = "EnableVibration";
    [SerializeField] private bool _defaultState = true;

    private void Awake()
    {
        if (PlayerPrefsSafe.GetBool(_playerPrefsKey, _defaultState) == false)
        {
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        _damageableObject.onTakeDamage += Vibrate;
    }

    private void OnDisable()
    {
        _damageableObject.onTakeDamage -= Vibrate;
    }

    private void Vibrate(float damage)
    {
        MMVibrationManager.Haptic(_vibrationType);
    }
}