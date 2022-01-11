using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindClosestTransform(this Transform transform, Transform[] transforms)
    {
        Transform closestTransform = null;
        var closestDistanceSqr = Mathf.Infinity;

        foreach (var potentiaTransform in transforms)
        {
            if (potentiaTransform == null || potentiaTransform.gameObject != null &&
                potentiaTransform.gameObject.activeSelf == false) continue;

            var directionToTarget = potentiaTransform.position - transform.position;
            var sqrDirectionToTarget = directionToTarget.sqrMagnitude;

            if (sqrDirectionToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDirectionToTarget;
                closestTransform = potentiaTransform;
            }
        }

        return closestTransform;
    }

    public static bool IsInFiendOfView(this Transform transform, Transform target, float radius, LayerMask layerMask)
    {
        var rayDirection = (target.position - transform.position).normalized;
        RaycastHit2D raycastHit2D;
        GameObject hitObject, targetObject;

        raycastHit2D = Physics2D.Raycast(transform.position, rayDirection, radius, layerMask);

        targetObject = target.gameObject;
        hitObject = raycastHit2D.collider.gameObject;

        return hitObject == targetObject;
    }
}