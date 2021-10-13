using System.Collections;
using UnityEngine;

public class KillableTargetDetection : TargetDetectionCore
{
    [Header("Targets")] [SerializeField] private KillableTarget[] _killableTargets;
    private KillableTarget _closestTarget;

    public KillableTarget ClosestTarget => _closestTarget;

    protected override void Awake()
    {
        base.Awake();

        _closestTarget = _killableTargets[0];
    }

    protected override IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            _closestTarget = FindClosestTarget(_killableTargets);

            yield return new WaitForSeconds(_findTargetDelay);
        }
    }

    private KillableTarget FindClosestTarget(KillableTarget[] killableTargets)
    {
        Transform[] targetTransforms = SelectTransforms(killableTargets);

        Transform closestTransform = _transform.FindClosestTransform(targetTransforms);

        return FindFirst(killableTargets, closestTransform);
    }

    private Transform[] SelectTransforms(KillableTarget[] killableTargets)
    {
        Transform[] transforms = new Transform[killableTargets.Length];
        
        for (int i = 0; i < killableTargets.Length; i++)
        {
            transforms[i] = killableTargets[i].Transform;
        }

        return transforms;
    }

    private KillableTarget FindFirst(KillableTarget[] killableTargets, Transform transform)
    {
        for (int i = 0; i < killableTargets.Length; i++)
        {
            if (_killableTargets[i].Transform == transform)
            {
                return _killableTargets[i];
            }
        }
        
        return null;
    }
}