using System.Linq;
using UnityEngine;

public static class KillableTargetExtensions
{
    public static KillableTarget FindClosestTarget(this KillableTarget killableTarget, KillableTarget[] killableTargets)
    {
        Transform[] targetTransforms = killableTargets.Select(x => x._transform).ToArray();
        
        Transform closestTransform = killableTarget._transform.FindClosestTransform(targetTransforms);
        
        return killableTargets.First(x => x._transform == closestTransform );
    }
}