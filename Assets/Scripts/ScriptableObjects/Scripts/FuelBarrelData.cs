using UnityEngine;

[CreateAssetMenu(fileName = "FuelBarrelData", menuName = "ScriptableObjects/FuelBarrelData")]
public class FuelBarrelData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _explodeDelay = 4f;
    [SerializeField] private int _maxTakeDamageNumber = 2;
    [SerializeField] private Pools _smoke;
    
    public float ExplodeDelay => _explodeDelay;
    public int MAXTakeDamageNumber => _maxTakeDamageNumber;
    public Pools Smoke => _smoke;

}
