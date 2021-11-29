using UnityEngine;

[CreateAssetMenu(fileName = "HealthBonusItemData", menuName = "ScriptableObjects/HealthBonusItemData")]
public class HealthBonusItemData : BonusItemData
{
    [Header("Preferences")] 
    [SerializeField] private float _minHealth = 10f;
    [SerializeField] private float _maxHealth = 40f;

    [Header("Apply effect")]
    public Pools applyEffect;
    
    public float MINHealth => _minHealth;
    public float MAXHealth => _maxHealth;
    
}
