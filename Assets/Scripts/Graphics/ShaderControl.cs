using System;
using UnityEngine;

public class ShaderControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _renderer;

    [Header("Preferences")]
    [SerializeField] private Material _defaultMaterial;

    private const string KEY = "EnableShadersValue";

    private void Awake()
    {
        if (PlayerPrefsSafe.GetBool(KEY, true) == false)
        {
            _renderer.material = _defaultMaterial;
        }
    }
}
