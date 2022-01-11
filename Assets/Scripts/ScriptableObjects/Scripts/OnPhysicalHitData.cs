using UnityEngine;

[CreateAssetMenu(fileName = "OnPhysicalHitData", menuName = "ScriptableObjects/OnPhysicalHitData")]
public class OnPhysicalHitData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _minDamageImpulse = 40f;
    [SerializeField] private float _damageAmplifier = 0.3f;
    public LayerMask layerMask;

    public float MINDamageImpulse => _minDamageImpulse;
    public float DamageAmplifier => _damageAmplifier;
}