using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectOnDamageColorData", menuName = "ScriptableObjects/ObjectOnDamageColorData")]
public class ObjectOnDamageColorData : ScriptableObject
{
    [Header("Preferences")]
    public Color onDamageColor;
    [SerializeField] private float _duration = 0.2f;
    private float _halfDuration;
    public float Duration => _duration;
    public float HalfDuration => _halfDuration;

    private void Awake()
    {
        _halfDuration = _duration / 2f;
    }
}