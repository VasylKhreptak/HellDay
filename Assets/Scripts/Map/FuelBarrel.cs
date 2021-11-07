using DG.Tweening;
using UnityEngine;

public class FuelBarrel : DestroyableObject
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _cameraImpactRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _upwardsModifier;
    [SerializeField] private ForceMode2D _forceMode2D;
    [SerializeField] private AnimationCurve _forceCurve;
    [SerializeField] private float _chainExplosionDelay = 0.5f;

    [Header("Camera Shake")]
    [SerializeField] private AnimationCurve  _shakeCurve;
    [SerializeField] private float _maxCameraShakeIntensity = 11f;

    [Header("Entity Damage")] 
    [SerializeField] private float _maxEntityDamage = 100f;
    [SerializeField] private AnimationCurve _entityDamageCurve;

    [Header("Physical Object Damage")] 
    [SerializeField] private float _maxObjectDamage = 25f;
    [SerializeField] private AnimationCurve _objectDamageCurve;
    
    public override void DestroyActions()
    {
        Explode();
        
        Destroy(gameObject);

        _objectPooler.GetFromPool(Pools.BarrelExplosion, _transform.position, Quaternion.identity);
    }

    private  void Explode()
    {
        Messenger<float>.Broadcast(GameEvent.SHAKE_CAMERA, GetEvaluatedCurveValue(
            _playerTransform.position, _transform.position, _shakeCurve, _maxCameraShakeIntensity,
            _cameraImpactRadius));
        
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(_transform.position,
            _explosionRadius, _layerMask);
        
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            InteractWithCollider(overlappedColliders[i]);
        }
    }

    private void InteractWithCollider(Collider2D collider2D)
    {
        SpreadOutObject(collider2D);

        if (collider2D.isActiveAndEnabled == false) return;

        if (collider2D.CompareTag("FuelBarrel"))
        {
            ExplodeChainedBarrel(collider2D);
        }
        else if (collider2D.TryGetComponent(out KillableTarget target))
        {
            target.Killable.TakeDamage(GetEvaluatedCurveValue(target.Transform.position, 
                _transform.position, _entityDamageCurve, _maxEntityDamage, _explosionRadius));
        }
        else if (collider2D.TryGetComponent(out DestroyableObject destroyableObject))
        {
            destroyableObject.TakeDamage( GetEvaluatedCurveValue(destroyableObject.Transform.position,
                _transform.position, _objectDamageCurve, _maxObjectDamage, _explosionRadius));
        }
    }

    private void SpreadOutObject(Collider2D collider2D)
    {
        Rigidbody2D rigidbody2D = collider2D.attachedRigidbody;

        if (rigidbody2D != null)
        {
            rigidbody2D.AddExplosionForce(_explosionForce, _transform.position, _explosionRadius,
                _forceCurve, _upwardsModifier, _forceMode2D);
        }
    }

    private void ExplodeChainedBarrel(Collider2D collider2D)
    {
        this.DOWait(_chainExplosionDelay).OnComplete(() =>
        {
            if (collider2D != null && 
                collider2D.TryGetComponent(out FuelBarrel fuelBarrel))
            {
                fuelBarrel.DestroyActions();
            }
        });
    }

    private float GetEvaluatedCurveValue(Vector3 target, Vector3 expPosition, AnimationCurve curve, 
        float maxValue, float impactRadius)
    {
        float distance = Vector3.Distance(target, expPosition);

        return curve.Evaluate(distance / impactRadius) * maxValue;
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _playerTransform == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _cameraImpactRadius);
    }
}