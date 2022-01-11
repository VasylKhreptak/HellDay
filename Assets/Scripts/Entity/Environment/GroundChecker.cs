using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Data")]
    [SerializeField] private GroundCheckerData _data;

    public bool IsGrounded()
    {
        Vector2 origin1, origin2;

        var halfDistanceBtwnRays = _data.DisBetweenRays / 2;

        origin1 = _transform.position - new Vector3(halfDistanceBtwnRays, 0);
        origin2 = _transform.position + new Vector3(halfDistanceBtwnRays, 0);

        return Physics2D.Raycast(origin1, Vector2.down, _data.RayHeight, _data.layerMask) ||
               Physics2D.Raycast(origin2, Vector2.down, _data.RayHeight, _data.layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null) return;

        Vector2 origin1, origin2;

        var halfDistacneBtwnRays = _data.DisBetweenRays / 2;

        origin1 = _transform.position - new Vector3(halfDistacneBtwnRays, 0);
        origin2 = _transform.position + new Vector3(halfDistacneBtwnRays, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin1, Vector3.down * _data.RayHeight);
        Gizmos.DrawRay(origin2, Vector3.down * _data.RayHeight);
    }
}