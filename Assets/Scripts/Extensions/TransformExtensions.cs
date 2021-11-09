using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindClosestTransform(this Transform transform, Transform[] transforms)
    {
        Transform closestTransform = null;
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i] == null)
            {
                continue;
            }
            
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
    
    public static bool IsInFiendOfView(this Transform transform, Transform target, float radius, LayerMask layerMask)
    {
        Vector3 rayDirection  = (target.position - transform.position).normalized;
        RaycastHit2D raycastHit2D;
        GameObject hitObject, targetObject;

        raycastHit2D = Physics2D.Raycast(transform.position, rayDirection, radius, layerMask);
        
        targetObject = target.gameObject;
        hitObject = raycastHit2D.collider.gameObject;

        return hitObject == targetObject;
    }
}
