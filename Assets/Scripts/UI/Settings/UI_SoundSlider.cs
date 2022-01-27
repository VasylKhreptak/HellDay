public class UI_SoundSlider : UI_VolumeSlider
{
    private const string KEY = "SoundVolumeValue";

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