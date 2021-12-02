using UnityEngine;

[CreateAssetMenu(fileName = "GroundCheckerData", menuName = "ScriptableObjects/GroundCheckerData")]
public class GroundCheckerData : EnvironmentCheckerCoreData
{
    [Header("Preferences")] 
    [SerializeField] private float _rayHeight = 0.1f;
    [SerializeField] private float _disBetweenRays = 0.3f;

    public float RayHeight => _rayHeight;
    public float DisBetweenRays => _disBetweenRays;
}
