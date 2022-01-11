using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTargetDetection : TargetDetectionCore
{
    [Header("Targets")]
    [SerializeField] private DamageableTarget[] _damageableTargets;

    [HideInInspector] public DamageableTarget _closestTarget;


#if UNITY_EDITOR
    [SerializeField] private LayerMask _findLayerMask;
#endif

    protected override void Awake()
    {
        base.Awake();

        _closestTarget = _damageableTargets[0];
    }

    protected override IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            _closestTarget = FindClosestTarget(_damageableTargets);

            yield return new WaitForSeconds(_data.FindTargetDelay);
        }
    }

    private DamageableTarget FindClosestTarget(DamageableTarget[] damageableTargets)
    {
        var targetTransforms = SelectTransforms(damageableTargets);

        var closestTransform = _transform.FindClosestTransform(targetTransforms);

        return FindFirst(damageableTargets, closestTransform);
    }

    private Transform[] SelectTransforms(DamageableTarget[] damageableTargets)
    {
        var transforms = new Transform[damageableTargets.Length];

        for (var i = 0; i < damageableTargets.Length; i++) transforms[i] = damageableTargets[i].Transform;

        return transforms;
    }

    private DamageableTarget FindFirst(DamageableTarget[] damageableTargets, Transform transform)
    {
        foreach (var target in damageableTargets)
            if (target.Transform == transform)
                return target;

        return null;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (_closestTarget == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_closestTarget.Transform.position,
            Vector2.one);

        Gizmos.DrawLine(_transform.position, _closestTarget.Transform.position);
    }

#if UNITY_EDITOR

    public void FindKillableTargets()
    {
        var allDamageableTargets = FindObjectsOfType<DamageableTarget>();
        var damageableTargets = new List<DamageableTarget>();

        foreach (var potentialTarget in allDamageableTargets)
            if (_findLayerMask.ContainsLayer(potentialTarget.gameObject.layer))
                damageableTargets.Add(potentialTarget);

        _damageableTargets = damageableTargets.ToArray();
    }

#endif
}