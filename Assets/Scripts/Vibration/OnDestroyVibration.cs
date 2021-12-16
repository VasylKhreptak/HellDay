using MoreMountains.NiceVibrations;
using UnityEngine;

public class OnDestroyVibration : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private TextAsset _AHAP;
    [SerializeField] private MMNVAndroidWaveFormAsset _destroyWaweForm;
    
    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false) return;
        
        MMVibrationManager.AdvancedHapticPattern(_AHAP.text,_destroyWaweForm.WaveForm.Pattern, 
            _destroyWaweForm.WaveForm.Amplitudes, -1, 
            null,null, null, 
            -1, HapticTypes.LightImpact, this);        
    }
}
