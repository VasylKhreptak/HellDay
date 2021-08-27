using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed = 5f;
    private float _previousMovementSpeed;
    [SerializeField] private float _minJumpVelocity = 15f;
    [SerializeField] private float _maxJumpVelocity = 30f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _horizontalSensetivity = 0.5f;
    [SerializeField] private float _verticalSensetivity = 0.8f;
    
    [Header("Ground check")] [SerializeField]
    private float _horizontalOffSet = 0.5f;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _heightRayCast = 1f;

    private void Awake()
    {
        _previousMovementSpeed = _movementSpeed;
        
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
    }
    
    private void OnPlayerGetUp()
    {
        _movementSpeed = _previousMovementSpeed;
    }

    private void OnPlayerSitDown()
    {
        _movementSpeed = 0;
    }

    private void Update()
    {
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
            _rigidbody2D.velocity = 
                new Vector2(_rigidbody2D.velocity.x,  
                     Mathf.Clamp(_maxJumpVelocity * _joystick.Vertical, _minJumpVelocity, _maxJumpVelocity));
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