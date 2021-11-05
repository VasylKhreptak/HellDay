using UnityEngine;

public class CommonZombieAtack : ZombieAtackCore
{
    [Header("Preferences")]
    [SerializeField] protected float _biteRadius;
    [SerializeField] protected Transform _atackCenter;
    [SerializeField] protected LayerMask _killableEntityLayerMask;

    protected virtual void SpawnAtackParticles()
    {
        _objectPooler.GetFromPool(Pools.ZombieBiteParticle, _atackCenter.position, Quaternion.identity);
    }

    protected override bool CanAtack()
    {
        Transform target = _killableTargetDetection.ClosestTarget.Transform;
        
        if (target == null || target.gameObject.activeSelf == false)
        {
            return false;
        }
        
        Collider2D collider2D = Physics2D.OverlapCircle(_atackCenter.position, _biteRadius, _killableEntityLayerMask);

        return collider2D != null;
    }

    protected override void Atack()
    {
        _audio.PlaBiteSound();
        
        _killableTargetDetection.ClosestTarget.Killable.TakeDamage(_damage);
        
        SpawnAtackParticles();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        if (_atackCenter == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_atackCenter.position, _biteRadius);
    }
}
