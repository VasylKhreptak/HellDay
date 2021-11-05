using UnityEngine;

public class GreenZombieAtack : ZombieAtackCore
{
    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius = 7f;
    [SerializeField] private LayerMask _environmentLayerMask;
    
    [Header("References")]
    [SerializeField] private Zombie _zombie;

    protected override bool CanAtack()
    {
        Transform target = _killableTargetDetection.ClosestTarget.Transform;
        
        return _transform.ContainsTransform(_explosionRadius, target) && 
               _transform.IsInFiendOfView(target, _explosionRadius, _environmentLayerMask);
    }

    protected override void Atack()
    {
        _audio.PlaBiteSound();
        
        _killableTargetDetection.ClosestTarget.Killable.TakeDamage(_damage);
        
        Destroy(gameObject);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
}
