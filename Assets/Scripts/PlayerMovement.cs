using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _jumpVelocity = 10f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _horizontalSensetivity = 0.5f;
    [SerializeField] private float _verticalSensetivity = 0.8f;

    [Header("Animator")] [SerializeField] private Animator _playerAnimator;

    [Header("Ground check")] [SerializeField]
    private float _horizontalOffSet = 0.5f;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _heightRayCast = 1f;

    private void Update()
    {
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));

        if (_joystick.Horizontal == 0) return;

        HorizontalMovement();

        VerticalMovement();

        if (_rigidbody2D.velocity.x != 0)
        {
            SetFaceDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
        }
    }

    private void HorizontalMovement()
    {
        if (Mathf.Abs(_joystick.Horizontal) > _horizontalSensetivity)
        {
            _rigidbody2D.velocity = new Vector2(_joystick.Horizontal * _movementSpeed, _rigidbody2D.velocity.y);
        }
    }

    private void VerticalMovement()
    {
        if (_joystick.Vertical > _verticalSensetivity && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        }
    }

    private void SetFaceDirection(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private bool IsGrounded()
    {
        Vector3 position = transform.position;

        if (Physics2D.Raycast(position + Vector3.right * _horizontalOffSet,
                Vector2.down, _heightRayCast, _layerMask) ||
            Physics2D.Raycast(position + Vector3.left * _horizontalOffSet,
                Vector2.down, _heightRayCast, _layerMask))
        {
            return true;
        }

        return false;
    }
}