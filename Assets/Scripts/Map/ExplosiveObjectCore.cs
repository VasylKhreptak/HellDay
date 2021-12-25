using System;
using DG.Tweening;
using UnityEngine;

public class ExplosiveObjectCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;

    [Header("Data")]
    [SerializeField] private ExplosiveObjectCoreData _explosiveObjData;

    public static Action onPlayedLoudSound;
    public static Action<Vector3, float, float> onCameraShake;


    protected void Explode()
    {
        onPlayedLoudSound?.Invoke();

        onCameraShake?.Invoke(_transform.position, _explosiveObjData.MAXCameraShakeIntensity,
            _explosiveObjData.ShakeDuration);

        var overlappedColliders = Physics2D.OverlapCircleAll(_transform.position,
            _explosiveObjData.ExplosionRadius, _explosiveObjData.layerMask);

        foreach (var coll in overlappedColliders) InteractWithCollider(coll);
    }

    protected void InteractWithCollider(Collider2D collider2D)
    {
        SpreadOutObject(collider2D);

        if (collider2D.isActiveAndEnabled == false) return;

        if (collider2D.CompareTag("FuelBarrel")) ExplodeChainedObject(collider2D);
        else if (collider2D.TryGetComponent(out IDamageable target))
            target.TakeDamage(_explosiveObjData.damageCurve.Evaluate(collider2D.transform.position,
                _transform.position, _explosiveObjData.MAXDamage, _explosiveObjData.ExplosionRadius));
    }

    protected void SpreadOutObject(Collider2D collider2D)
    {
        var rigidbody2D = collider2D.attachedRigidbody;

        if (rigidbody2D != null)
            rigidbody2D.AddExplosionForce(_explosiveObjData.ExplosionForce, _transform.position,
                _explosiveObjData.ExplosionRadius, _explosiveObjData.forceCurve,
                _explosiveObjData.UpwardsModifier, _explosiveObjData.ForceMode2D);
    }

    protected void ExplodeChainedObject(Collider2D collider2D)
    {
        this.DOWait(_explosiveObjData.ChainExplosionDelay).OnComplete(() => {
            if (collider2D != null) collider2D.gameObject.SetActive(false);
        });
    }

    protected void OnDrawGizmosSelected()
    {
        if (_transform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosiveObjData.ExplosionRadius);
    }
}
