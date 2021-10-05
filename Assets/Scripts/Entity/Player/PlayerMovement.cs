using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _minJumpVelocity = 15f;
    [SerializeField] private float _maxJumpVelocity = 30f;

    [Header("joystick")] [SerializeField] private Joystick _joystick;
    [SerializeField] private float _horizontalSensetivity = 0.5f;
    [SerializeField] private float _verticalSensetivity = 0.8f;

    [Header("General preferences")] [SerializeField]
    private bool _canMove = true;

    [Header("Ground checker")] [SerializeField]
    private GroundChecker _groundChecker;

    [Range(-1, 1)] public static int movementDirection;

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.AddListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
        Messenger<float>.AddListener(GameEvent.PLAYER_MOVEMENT_IMPACT, OnPlayerMovementImpact);

        SetDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvent.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_LEG_PUNCH, OnLegPunched);
        Messenger<float>.RemoveListener(GameEvent.PLAYER_MOVEMENT_IMPACT, OnPlayerMovementImpact);
    }

    private void OnPlayerGetUp()
    {
        _canMove = true;
    }

    private void OnPlayerMovementImpact(float percentage)
    {
        _movementSpeed -= _movementSpeed * percentage / 100f;
        _minJumpVelocity -= _minJumpVelocity * percentage / 100f;
        _maxJumpVelocity -= _maxJumpVelocity * percentage / 100f;
    }

    private void OnPlayerSitDown()
    {
        _canMove = false;
    }

    private void OnLegPunched(float punchDuration)
    {
        StartCoroutine(OnLegPunchedRoutine(punchDuration));
    }

    private IEnumerator OnLegPunchedRoutine(float punchDuration)
    {
        _canMove = false;

        yield return new WaitForSeconds(punchDuration);

        _canMove = true;
    }

    private void Update()
    {
        if (_joystick.Horizontal == 0 || _canMove == false) return;

        HorizontalMovement();

        VerticalMovement();

        if (_rigidbody2D.velocity.x != 0)
        {
            SetDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
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
        if (_joystick.Vertical > _verticalSensetivity && _groundChecker.isGrounded == true)
        {
            _rigidbody2D.velocity =
                new Vector2(_rigidbody2D.velocity.x,
                    Mathf.Clamp(_maxJumpVelocity * _joystick.Vertical, _minJumpVelocity, _maxJumpVelocity));
        }
    }

    private void SetDirection(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);

        movementDirection = direction;
    }
}