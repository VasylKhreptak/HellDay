using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveObjectCoreData", menuName = "ScriptableObjects/ExplosiveObjectCoreData")]
public class ExplosiveObjectCoreData : ScriptableObject
{
    [Header("Explosion Preferences")]
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 300f;
    [SerializeField] private float _upwardsModifier = 0.1f;
    [SerializeField] private ForceMode2D _forceMode2D = ForceMode2D.Impulse;
    
    [Header("Chain Effect Preferences")]
    [SerializeField] private float _chainExplosionDelay = 0.3f;
    
    [Header("Damage")]
    [SerializeField] private float _maxDamage = 130f;
    
    [Header("Camera Shake Preferences")]
    [SerializeField] private float _maxCameraShakeIntensity = 13f;
    [SerializeField] private float _shakeDuration = 0.7f;
    
    public LayerMask layerMask;
    public AnimationCurve forceCurve;
    public AnimationCurve damageCurve;

    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;
    public float UpwardsModifier => _upwardsModifier;
    public ForceMode2D ForceMode2D => _forceMode2D;
    public float ChainExplosionDelay => _chainExplosionDelay;
    public float MAXDamage => _maxDamage;
    public float MAXCameraShakeIntensity => _maxCameraShakeIntensity;
    public float ShakeDuration => _shakeDuration;
}
