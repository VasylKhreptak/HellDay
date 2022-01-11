using UnityEngine;

[CreateAssetMenu(fileName = "AIMovementCoreData", menuName = "ScriptableObjects/AIMovementCoreData")]
public class AIMovementCoreData : ScriptableObject
{
    [Header("Movement Preferences")] [SerializeField]
    private float _jumpForce = 18f;

    [SerializeField] private ForceMode2D _forceMode2D;
    [SerializeField] private float _maxHorVelocity = 5f;

    [Header("Environment Check Delay")]
    [SerializeField] private float _changeDirectionDelay = 3f;
    [SerializeField] private float _defaultDelay = 0.5f;
    [SerializeField] private float _obstacleCheckDelay = 0.3f;

    public float JumpForce => _jumpForce;
    public ForceMode2D ForceMode2D => _forceMode2D;
    public float MAXHorVelocity => _maxHorVelocity;
    public float ChangeDirectionDelay => _changeDirectionDelay;
    public float DefaultDelay => _defaultDelay;
    public float ObstacleCheckDelay => _obstacleCheckDelay;
}