using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindClosestTransform(this Transform transform, Transform[] transforms)
    {
        Transform closestTransform = null;
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < transforms.Length; i++)
        {
            Vector3 directionToTarget = transforms[i].position - transform.position;
            float sqrDirectionToTarget = directionToTarget.sqrMagnitude;

            if (sqrDirectionToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDirectionToTarget;
                closestTransform = transforms[i];
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
        
        return (transform.position - target.position).sqrMagnitude < radius * radius;
    }
}
