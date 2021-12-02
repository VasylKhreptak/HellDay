using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentCheckerCoreData", menuName = "ScriptableObjects/EnvironmentCheckerCoreData")]
public class EnvironmentCheckerCoreData : ScriptableObject
{
    [Header("Detection LayerMask")] 
    public LayerMask layerMask;
}
