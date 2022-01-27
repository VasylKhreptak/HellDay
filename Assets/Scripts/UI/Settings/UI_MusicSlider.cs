using UnityEngine;

public class UI_MusicSlider : UI_VolumeSlider
{
    private const string KEY = "MusicVolumeValue";
    
    private void Awake()
    {
        _slider.value = PlayerPrefsSafe.GetFloat(KEY, 0);
    }

    private void OnDestroy()
    {
        PlayerPrefsSafe.SetFloat(KEY, _slider.value);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefsSafe.SetFloat(KEY, _slider.value);
        }
    }
}