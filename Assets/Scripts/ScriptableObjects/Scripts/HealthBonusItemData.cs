using UnityEngine;

[CreateAssetMenu(fileName = "HealthBonusItemData", menuName = "ScriptableObjects/HealthBonusItemData")]
public class HealthBonusItemData : BonusItemData
{
    [Header("Preferences")]
    [SerializeField] private float _minHealth = 10f;
    [SerializeField] private float _maxHealth = 40f;

    [Header("Apply effect")]
    public Pools applyEffect;
    public Pools healthPopup = Pools.DamagePopup;

    [Header("Popup Color")]
    public UnityEngine.Color popupColor = UnityEngine.Color.green;

    public float MINHealth => _minHealth;
    public float MAXHealth => _maxHealth;
}