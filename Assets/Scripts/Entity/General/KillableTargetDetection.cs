using System;
using System.Collections;
using UnityEngine;

public class KillableTargetDetection : TargetDetectionCore
{
    [Header("Targets")] [SerializeField] private KillableTarget[] _killableTargets;

    public KillableTarget _closestKillableTarget { get; private set; }

    private void Awake()
    {
        _closestKillableTarget = _killableTargets[0];
    }

    protected override IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            _closestKillableTarget = _closestKillableTarget.FindClosestTarget(_killableTargets);

            yield return new WaitForSeconds(_findTargetDelay);
        }
    }
}