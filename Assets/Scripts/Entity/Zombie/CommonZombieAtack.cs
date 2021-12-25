using UnityEngine;

public class CommonZombieAtack : ZombieAtackCore
{
    [Header("References")]
    [SerializeField] protected Transform _atackCenter;

    [Header("Common Data")]
    [SerializeField] private CommonZombieAtackData _commonZombieData;

    protected virtual void SpawnAtackParticle()
    {
        _objectPooler.GetFromPool(Pools.ZombieBiteParticle, _atackCenter.position, Quaternion.identity);
    }

    protected override bool CanAtack()
    {
        var target = _damageableTargetDetection._closestTarget.Transform;

        if (target == null || target.gameObject.activeSelf == false) return false;

        var collider2D = Physics2D.OverlapCircle(_atackCenter.position,
            _commonZombieData.BiteRadius, _commonZombieData.entityLayerMask);

        return collider2D != null;
    }

    protected override void Atack()
    {
        _audio.PlaBiteSound();

        _damageableTargetDetection._closestTarget.Damageable.TakeDamage(_commonZombieData.DamageValue);

        SpawnAtackParticle();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (_atackCenter == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_atackCenter.position, _commonZombieData.BiteRadius);
    }
}
