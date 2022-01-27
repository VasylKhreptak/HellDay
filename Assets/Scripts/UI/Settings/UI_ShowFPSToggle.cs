using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShowFPSToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _fpsGameObject;
    [SerializeField] private Toggle _toggle;

    private const string KEY = "ShowFPSValue";

    private void Awake()
    {
        _toggle.isOn = PlayerPrefsSafe.GetBool(KEY, false);
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
        _fpsGameObject.SetActive(state);
        
        PlayerPrefsSafe.SetBool(KEY, state);
    }
}
