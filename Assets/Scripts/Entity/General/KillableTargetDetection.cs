using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public class KillableTargetDetection : TargetDetectionCore
{
    [Header("Targets")] 
    [SerializeField] private KillableTarget[] _killableTargets;
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
        Transform[] targetTransforms = killableTargets.Select(x => x.Transform).ToArray();
        
        Transform closestTransform = _transform.FindClosestTransform(targetTransforms);

        return killableTargets.First(x => x.Transform == closestTransform);
    }
}