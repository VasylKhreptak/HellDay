using Unity.VisualScripting;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindClosestTransform(this Transform transform, Transform[] transforms)
    {
        Transform closestTransform = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform potentialTransform in transforms)
        {
            if (potentialTransform == null || potentialTransform.gameObject.activeSelf == false)
            {
                continue;
            }
            
            Vector3 directionToTarget = potentialTransform.position - transform.position;
            float sqrDirectionToTarget = directionToTarget.sqrMagnitude;

            if (sqrDirectionToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDirectionToTarget;
                closestTransform = potentialTransform;
            }
        }

        return closestTransform;
    }

    public static bool ContainsTransform(this Transform transform, float radius, Transform target)
    {
        if (target == null)
        {
            return false;
        }
        
        return Vector2.Distance(transform.position, target.position) < radius;
    }
}
