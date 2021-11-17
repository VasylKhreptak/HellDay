using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonTargetDetection : TargetDetectionCore
{
    [Header("Targets")]
    [SerializeField] private Transform[] _targets;

    public Transform ClosestTarget { get; private set; }

#if UNITY_EDITOR
    [SerializeField] private LayerMask _findLayerMask;
#endif
    
    protected override IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            ClosestTarget = _transform.FindClosestTransform(_targets);
            
            yield return new WaitForSeconds(_findTargetDelay);
        }   
    }

#if UNITY_EDITOR

    public void FindTargets()
    {
        KillableTarget[] allTargets = FindObjectsOfType<KillableTarget>();
        var targets = new List<Transform>();

        foreach (var potentialTarget in allTargets)
        {
            if (_findLayerMask.ContainsLayer(potentialTarget.gameObject.layer))
            {
                targets.Add(potentialTarget.Transform);
            }
        }

        _targets = targets.ToArray();
    }
    
#endif
}
