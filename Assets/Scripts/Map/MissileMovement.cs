using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Data")] 
    [SerializeField] private MissileMovementData _data;

    private Transform _target;

    private void Awake()
    {
        _target = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        
        Vector2 dirToTarget = _target.position - _transform.position;
        dirToTarget.Normalize();

        float rotateAmount = Vector3.Cross(dirToTarget, _transform.up).z;
        _rigidbody2D.angularVelocity = -rotateAmount * _data.RotationSpeed;
        _rigidbody2D.velocity = _transform.up * _data.MovementSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _target == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_transform.position, _target.position);
    }
}
