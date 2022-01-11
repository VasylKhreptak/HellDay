using MoreMountains.NiceVibrations;
using UnityEngine;

public class OnTakeDamageVibration : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Preferences")]
    [SerializeField] private HapticTypes _vibrationType = HapticTypes.LightImpact;

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