using DG.Tweening;
using UnityEngine;

public class ExplosiveObjectCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    
    [Header("Explosion Preferences")] 
    [SerializeField] protected float _explosionRadius;
    [SerializeField] protected float _explosionForce;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected float _upwardsModifier;
    [SerializeField] protected ForceMode2D _forceMode2D;
    [SerializeField] protected AnimationCurve _forceCurve;
    [SerializeField] protected float _chainExplosionDelay = 0.5f;
    [SerializeField] protected Pools _fuelBarrelExplosion = Pools.FuelBarrelExplosion;
    
    [Header("Entity Damage")] 
    [SerializeField] protected float _maxEntityDamage = 100f;
    [SerializeField] protected AnimationCurve _entityDamageCurve;

    [Header("Physical Object Damage")] 
    [SerializeField] protected float _maxObjectDamage = 25f;
    [SerializeField] protected AnimationCurve _objectDamageCurve;
    
    [Header("Camera Shake")] 
    [SerializeField] protected float _maxCameraShakeIntensity = 13f;
    [SerializeField] protected float _shakeDuration = 0.7f;
    
    protected  void Explode()
    {
        Messenger.Broadcast(GameEvents.PLAYED_LOUD_AUDIO_SOURCE);
        Messenger<Vector3, float, float>.Broadcast(GameEvents.SHAKE_CAMERA, _transform.position,
            _maxCameraShakeIntensity, _shakeDuration);

        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(_transform.position,
            _explosionRadius, _layerMask);

        foreach (var coll in overlappedColliders)
        {
            InteractWithCollider(coll);
        }
    }

    protected void InteractWithCollider(Collider2D collider2D)
    {
        SpreadOutObject(collider2D);

        if (collider2D.isActiveAndEnabled == false) return;

        if (collider2D.CompareTag("FuelBarrel"))
        {
            ExplodeChainedObject(collider2D);
        }
        else if (collider2D.TryGetComponent(out KillableTarget target))
        {
            target.Killable.TakeDamage(_entityDamageCurve.Evaluate(target.Transform.position, 
                _transform.position, _maxEntityDamage, _explosionRadius));
        }
        else if (collider2D.TryGetComponent(out DestroyableObject destroyableObject))
        {
            destroyableObject.TakeDamage( _objectDamageCurve.Evaluate(destroyableObject.Transform.position,
                _transform.position, _maxObjectDamage, _explosionRadius));
        }
    }

    protected void SpreadOutObject(Collider2D collider2D)
    {
        Rigidbody2D rigidbody2D = collider2D.attachedRigidbody;

        if (rigidbody2D != null)
        {
            rigidbody2D.AddExplosionForce(_explosionForce, _transform.position, _explosionRadius,
                _forceCurve, _upwardsModifier, _forceMode2D);
        }
    }

    protected void ExplodeChainedObject(Collider2D collider2D)
    {
        this.DOWait(_chainExplosionDelay).OnComplete(() =>
        {
            if (collider2D != null)
            {
                collider2D.gameObject.SetActive(false);
            }
        });
    }
    
    protected void OnDrawGizmosSelected()
    {
        if (_transform == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
}
