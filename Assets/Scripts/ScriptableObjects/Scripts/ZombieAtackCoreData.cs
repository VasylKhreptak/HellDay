using UnityEngine;

public class ZombieAtackCoreData : ScriptableObject
{
    [Header("Damage Preferences")] 
    [SerializeField] private float _minDamage = 10f;
    [SerializeField] private float _maxDamage = 20f;
    [SerializeField] private float _atackDelay = 1f;

    public float MINDamage => _minDamage;
    public float MAXDamage => _maxDamage;
    public float AtackDelay => _atackDelay;

    public float DamageValue => Random.Range(_minDamage, _maxDamage);
}
