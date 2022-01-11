using UnityEngine;

[CreateAssetMenu(fileName = "MissileMovementData", menuName = "ScriptableObjects/MissileMovementData")]
public class MissileMovementData : ScriptableObject
{
    [Header("Movement Preferences")]
    [SerializeField] private float _rotationSpeed = 200f;
    [SerializeField] private float _movementSpeed = 7f;

    public float RotationSpeed => _rotationSpeed;
    public float MovementSpeed => _movementSpeed;
}