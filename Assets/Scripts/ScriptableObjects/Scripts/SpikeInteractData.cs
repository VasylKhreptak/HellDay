using UnityEngine;

[CreateAssetMenu(fileName = "SpikeInteractData", menuName = "ScriptableObjects/SpikeInteractData")]
public class SpikeInteractData : ScriptableObject
{
    [Header("Damage Preferences")]
    [SerializeField] private float _damageDelay = 1f;
    [SerializeField] private float _minDamage = 10f;
    [SerializeField] private float _maxDamage = 120f;
    
    [Header("Blood Particle")]
    public Pools blood = Pools.ZombieBiteParticle;
    
    [Header("Audio Preferences")]
    public AudioClip[] biteAudioClips;
    
    public float DamageDelay => _damageDelay;
    public float MINDamage => _minDamage;
    public float MAXDamage => _maxDamage;
}
