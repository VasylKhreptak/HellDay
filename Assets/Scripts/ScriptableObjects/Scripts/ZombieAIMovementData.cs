using UnityEngine;

[CreateAssetMenu(fileName = "ZombieAIMovementData", menuName = "ScriptableObjects/ZombieAIMovementData")]
public class ZombieAIMovementData : ScriptableObject
{
    [Header("Target Detection Preferences")]
    [SerializeField] private float _audioDetectionRadius;
    [SerializeField] private float _incDetectionRadiusDur = 10f;

    public float AudioDetectionRadius => _audioDetectionRadius;
    public float IncDetectionRadiusDur => _incDetectionRadiusDur;
}