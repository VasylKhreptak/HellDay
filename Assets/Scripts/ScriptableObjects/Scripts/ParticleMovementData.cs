using UnityEngine;

[CreateAssetMenu(fileName = "ParticleMovementData", menuName = "ScriptableObjects/ParticleMovementData")]
public class ParticleMovementData : ScriptableObject
{
    [Header("Horizontal Movement")]
    [SerializeField] private float _minHorizontalVelocity = -6f;
    [SerializeField] private float _maxHorizontalVelocity = 6f;

    [Header("Vertical Movement")]
    [SerializeField] private float _minVerticalVelocity = 1f;
    [SerializeField] private float _maxVerticalVelocity = 2f;

    [Header("Preferences")]
    [SerializeField] private float _torque = 3f;

    public float MINHorizontalVelocity => _minHorizontalVelocity;
    public float MAXHorizontalVelocity => _maxHorizontalVelocity;
    public float MINVerticalVelocity => _minVerticalVelocity;
    public float MAXVerticalVelocity => _maxVerticalVelocity;
    public float Torque => _torque;
}