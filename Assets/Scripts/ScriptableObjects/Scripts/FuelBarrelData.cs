using UnityEngine;

[CreateAssetMenu(fileName = "FuelBarrelData", menuName = "ScriptableObjects/FuelBarrelData")]
public class FuelBarrelData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _explodeDelay = 4f;
    [SerializeField] [Range(0f, 100f)] private float _healthPercentage = 50f;

    public float ExplodeDelay => _explodeDelay;
    public float HealthPercentage => _healthPercentage;
}