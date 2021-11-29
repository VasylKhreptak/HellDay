using UnityEngine;

public class GreenZombieAtack : ZombieAtackCore
{
    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius = 7f;
    [SerializeField] private LayerMask _environmentLayerMask;

    protected override bool CanAtack()
    {
        Transform target = _damageableTargetDetection._closestTarget.Transform;

        if (_transform == null || target == null) return false;
        
        return _transform.position.ContainsPosition(_explosionRadius, target.position) && 
               _transform.IsInFiendOfView(target, _explosionRadius, _environmentLayerMask);
    }

    protected override void Atack()
    {
        _audio.PlaBiteSound();
        
        _damageableTargetDetection._closestTarget.Damageable.TakeDamage(_damage);
        
        Destroy(gameObject);
    }

    protected void OnDrawGizmosSelected()
    {
        if(_transform == null ) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
}
