using UnityEngine;

public class GreenZombieAtack : ZombieAtackCore
{
    [Header("Green Zombie Data")] 
    [SerializeField] private GreenZombieAtackData _greenZombieData;
    

    protected override bool CanAtack()
    {
        Transform target = _damageableTargetDetection._closestTarget.Transform;

        if (_transform == null || target == null) return false;
        
        return _transform.position.ContainsPosition(_greenZombieData.ExplosionRadius, target.position) && 
               _transform.IsInFiendOfView(target, _greenZombieData.ExplosionRadius, _greenZombieData.environmentLayerMask);
    }

    protected override void Atack()
    {
        _audio.PlaBiteSound();
        
        _damageableTargetDetection._closestTarget.Damageable.TakeDamage(_greenZombieData.DamageValue);
        
        Destroy(gameObject);
    }

    protected void OnDrawGizmosSelected()
    {
        if(_transform == null ) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _greenZombieData.ExplosionRadius);
    }
}
