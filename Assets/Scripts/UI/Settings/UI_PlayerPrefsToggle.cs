using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerPrefsToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;

    [Header("Preferences")]
    [SerializeField] private string _key = "EnableShadersValue";
    [SerializeField] private bool _defaultValue;

    private void Awake()
    {
        _toggle.isOn = PlayerPrefsSafe.GetBool(_key, _defaultValue);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    protected virtual void OnValueChanged(bool state)
    {
        PlayerPrefsSafe.SetBool(_key, state);
    }
}
