using UnityEngine;

[CreateAssetMenu(fileName = "DamageableObjectData", menuName = "ScriptableObjects/DamageableObjectData")]
public class DamageableObjectData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private bool _canBeDestroyed = true;

    public float MAXHealth => _maxHealth;
    public bool CanBeDestroyed => _canBeDestroyed;
}