using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectOnDamageColorData", menuName = "ScriptableObjects/ObjectOnDamageColorData")]
public class ObjectOnDamageColorData : ScriptableObject
{
    [Header("Preferences")]
    public Color onDamageColor;
    [SerializeField] private float _duration = 0.2f;
    private float _halfDuration;
    [HideInInspector] public float halfDuration;
    public float Duration => _duration;
}
