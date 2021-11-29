using UnityEngine;

[CreateAssetMenu(fileName = "TargetDetectionData", menuName = "ScriptableObjects/TargetDetectionData")]
public class TargetDetectionData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _findTargetDelay = 1f;

    public float FindTargetDelay => _findTargetDelay;
}
