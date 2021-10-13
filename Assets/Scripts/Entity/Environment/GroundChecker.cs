using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")] 
    [SerializeField] private float _rayHeight = 1f;
    [SerializeField] private float _distanceBetweenRays = 0.5f;
    [SerializeField] private LayerMask _layerMask;

    public bool IsGrounded()
    {
        Vector2 origin1, origin2;

        origin1 = _transform.position - new Vector3(_distanceBetweenRays / 2, 0);
        origin2 = _transform.position + new Vector3(_distanceBetweenRays / 2, 0);

        return Physics2D.Raycast(origin1, Vector2.down, _rayHeight, _layerMask) ||
               Physics2D.Raycast(origin2, Vector2.down, _rayHeight, _layerMask);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_transform == null)
        {
            return;
        }

        Vector2 origin1, origin2;
        
        origin1 = _transform.position - new Vector3(_distanceBetweenRays / 2, 0);
        origin2 = _transform.position + new Vector3(_distanceBetweenRays / 2, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(origin1, Vector3.down * _rayHeight);
        Gizmos.DrawRay(origin2, Vector3.down * _rayHeight);
    }
#endif
}
