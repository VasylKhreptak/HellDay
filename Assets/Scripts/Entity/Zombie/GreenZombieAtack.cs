using DG.Tweening;
using UnityEngine;

public class GreenZombieAtack : ZombieAtackCore
{
    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius = 7f;

    [SerializeField] private Pools _zombieExplosionParticle = Pools.GreenZombieExplosion;
    [SerializeField] private float _damageDelay = 1f;
    [SerializeField] private LayerMask _environmentLayerMask;
    
    [Header("References")]
    [SerializeField] private Zombie _zombie;

    protected override bool CanAtack()
    {
        return IsTargetInRange() && IsTargetInFiendOfView();
    }

    private bool IsTargetInRange()
    {
        return _transform.ContainsTransform(_explosionRadius,
            _killableTargetDetection.ClosestTarget.Transform);
    }

    private bool IsTargetInFiendOfView()
    {
        RaycastHit2D raycastHit2D;
        GameObject hitObject, targetObject;

        raycastHit2D = Physics2D.Raycast(_transform.position,
            GetDirectionToTarget(),
            _explosionRadius, _environmentLayerMask);

        targetObject = _killableTargetDetection.ClosestTarget.Transform.gameObject;
        hitObject = raycastHit2D.collider.gameObject;

        return hitObject == targetObject;
    }

    private Vector2 GetDirectionToTarget()
    {
        Vector3 direction, targetPosition;

        targetPosition = _killableTargetDetection.ClosestTarget.Transform.position;
        
        direction = (targetPosition - _transform.position).normalized;

        return direction;
    }

    protected override void Atack()
    {
        _objectPooler.GetFromPool(_zombieExplosionParticle, _transform.position, Quaternion.identity);

        _transform.DOWait(_damageDelay, () =>
        {
            _killableTargetDetection.ClosestTarget.Killable.TakeDamage(_damage);
        });
        
        _zombie.SpawnBodyParts();
        
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
#endif
}
