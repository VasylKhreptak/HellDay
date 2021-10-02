using UnityEngine;
using System.Collections;

public class CommonTargetDetection : TargetDetectionCore
{
    [Header("Targets")]
    [SerializeField] private Transform[] _targets;

    public Transform closestTarget { get; private set; }

    protected override IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            closestTarget = _transform.FindClosestTransform(_targets);
            
            yield return new WaitForSeconds(_findTargetDelay);
        }   
    }
}
