using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TargetFramerateSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FPS_Controller _fpsController;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _tmp;

    [Header("Preferences")]
    [SerializeField] private int _defaultValue;

    private const string KEY = "FramerateValue";
    
    private void Awake()
    {
        int framerate = PlayerPrefsSafe.GetInt(KEY, _defaultValue);
        
        _slider.value = framerate;
        _tmp.text = framerate.ToString();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        _fpsController.SetTargetFrameRate((int)value);

        _tmp.text = ((int)value).ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefsSafe.SetInt(KEY, (int)_slider.value);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefsSafe.SetInt(KEY, (int)_slider.value);
        }
    }
}
