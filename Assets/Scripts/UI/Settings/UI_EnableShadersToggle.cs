using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnableShadersToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;

    private const string KEY = "EnableShadersValue";

    private void Awake()
    {
        _toggle.isOn = PlayerPrefsSafe.GetBool(KEY, true);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool state)
    {
        PlayerPrefsSafe.SetBool(KEY, state);
    }
}
