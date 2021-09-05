using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _minJumpVelocity = 15f;
    [SerializeField] private float _maxJumpVelocity = 30f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _horizontalSensetivity = 0.5f;
    [SerializeField] private float _verticalSensetivity = 0.8f;
    [SerializeField] private bool _canMove = true;
    [SerializeField] private GroundChecker _groundChecker;
    
    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.AddListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
    }

    private void OnPlayerGetUp()
    {
        _canMove = true;
    }

    private void OnPlayerSitDown()
    {
        _canMove = false;
    }

    private void OnLegPunched(float animationDuration)
    {
        StartCoroutine(OnLegPunchedCoroutine(animationDuration));
    }

    private IEnumerator OnLegPunchedCoroutine(float animationDuration)
    {
        _canMove = false;

        yield return new WaitForSeconds(animationDuration);

        _canMove = true;
    }

    private void Update()
    {
        if (_joystick.Horizontal == 0) return;

        if (!_canMove) return;

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
        if (_joystick.Vertical > _verticalSensetivity && _groundChecker.isGrounded)
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
}