using DG.Tweening;
using UnityEngine;

public class ExplosiveObjectCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;

    [Header("Data")] 
    [SerializeField] private ExplosiveObjectCoreData _explosiveObjData;
    
    protected  void Explode()
    {
        Messenger.Broadcast(GameEvents.PLAYED_LOUD_AUDIO_SOURCE);
        Messenger<Vector3, float, float>.Broadcast(GameEvents.SHAKE_CAMERA, _transform.position,
            _explosiveObjData.MAXCameraShakeIntensity, _explosiveObjData.ShakeDuration);

        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(_transform.position,
            _explosiveObjData.ExplosionRadius, _explosiveObjData.layerMask);

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
        else if (collider2D.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_explosiveObjData.damageCurve.Evaluate(collider2D.transform.position, 
                _transform.position, _explosiveObjData.MAXDamage, _explosiveObjData.ExplosionRadius));
        }
    }

    protected void SpreadOutObject(Collider2D collider2D)
    {
        Rigidbody2D rigidbody2D = collider2D.attachedRigidbody;

        if (rigidbody2D != null)
        {
            rigidbody2D.AddExplosionForce(_explosiveObjData.ExplosionForce, _transform.position,
                _explosiveObjData.ExplosionRadius, _explosiveObjData.forceCurve, 
                _explosiveObjData.UpwardsModifier, _explosiveObjData.ForceMode2D);
        }
    }

    protected void ExplodeChainedObject(Collider2D collider2D)
    {
        this.DOWait(_explosiveObjData.ChainExplosionDelay).OnComplete(() =>
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
        Gizmos.DrawWireSphere(_transform.position, _explosiveObjData.ExplosionRadius);
    }
}
