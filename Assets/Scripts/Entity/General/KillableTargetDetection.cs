using System.Collections;
using UnityEngine;
using System.Linq;

public class KillableTargetDetection : TargetDetectionCore
{
    [Header("Targets")] 
    [SerializeField] private KillableTarget[] _killableTargets;

    public KillableTarget _closestKillableTarget { get; private set; }

    private void Awake()
    {
        _closestKillableTarget = _killableTargets[0];
    }

    protected override IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            _closestKillableTarget = FindClosestTarget(_killableTargets);

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