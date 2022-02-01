using System;
using UnityEngine;

public class ShaderControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _renderer;

    [Header("Preferences")]
    [SerializeField] private Material _defaultMaterial;
    
    [Header("PlayerPrefs Preferences")]
    [SerializeField] private string _playerPrefsKey = "EnableShaders";
    [SerializeField] private bool _defaultState = true;
    
    private void Awake()
    {
        if (PlayerPrefsSafe.GetBool(_playerPrefsKey, _defaultState) == false)
        {
            _renderer.material = _defaultMaterial;
        }
    }
}
