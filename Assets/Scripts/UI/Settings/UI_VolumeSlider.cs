using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] protected Slider _slider;
    
    [Header("Preferences")]
    [SerializeField] private string _parameterName = "Music";

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float sliderValue)
    {
        _slider.value = sliderValue;
        
        _audioMixer.SetFloat(_parameterName, sliderValue);
    }
}
