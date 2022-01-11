using UnityEngine;

[CreateAssetMenu(fileName = "HumanAIMovementData", menuName = "ScriptableObjects/HumanAIMovementData")]
public class HumanAIMovementData : ScriptableObject
{
    [SerializeField] private float _detectionRadius = 5f;

    public float DetectionRadius => _detectionRadius;
}