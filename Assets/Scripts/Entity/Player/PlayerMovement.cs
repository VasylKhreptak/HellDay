using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GroundChecker _groundChecker;
    
    [Header("Preferences")]
    [SerializeField] private float _movementForce = 5f;
    [SerializeField] private float _minJumpForce = 15f;
    [SerializeField] private float _maxJumpForce = 30f;
    [SerializeField, Range(0f, 1f)] private float _horizontalSensetivity = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _verticalSensetivity = 0.8f;
    [SerializeField] private float _jumpDelay = 1f;
    [SerializeField] private float _maxHorVelocity = 5f;
    [SerializeField] private ForceMode2D _movementMode = ForceMode2D.Impulse;
    
    private bool _canMove = true;
    private bool _isJumpForbidden ;
    [Range(-1, 1)] private static int movementDirection;
    
    public static  int MovementDirection => movementDirection;
    
    private readonly float MIN_CHANGE_DIRECTION_SPEED = 0.1f;
    private readonly int UPDATE_FRAMERATE = 10;
    private Coroutine _configurableUpdate;
    private bool _isGrounded;

    private float _previousMovementForce,
                  _previousMinJumpForce,
                  _previousMaxJumpForce,
                  _previousMaxHorVelocity;

    private void Awake()
    {
        _previousMovementForce = _movementForce;
        _previousMinJumpForce = _minJumpForce;
        _previousMaxJumpForce = _maxJumpForce;
        _previousMaxHorVelocity = _maxHorVelocity;
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvents.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.AddListener(GameEvents.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.AddListener(GameEvents.PLAYER_LEG_KICK, OnLegPunched);
        Messenger<float>.AddListener(GameEvents.PLAYER_MOVEMENT_IMPACT, ImpactMovement);

        SetDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
        
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, UPDATE_FRAMERATE, () =>
        {
            if (IsJoystickPressed())
            {
                ConfigureFaceDirection();
            }
        });
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvents.PLAYER_GET_UP, OnPlayerGetUp);
        Messenger.RemoveListener(GameEvents.PLAYER_SIT_DOWN, OnPlayerSitDown);
        Messenger<float>.RemoveListener(GameEvents.PLAYER_LEG_KICK, OnLegPunched);
        Messenger<float>.RemoveListener(GameEvents.PLAYER_MOVEMENT_IMPACT, ImpactMovement);
        
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void OnPlayerGetUp()
    {
        _canMove = true;
    }

    private void ImpactMovement(float percentage)
    {
        RestoreMovementPreferences();
        
        float clampedPercentage = percentage / 100f;
        
        _movementForce -= _movementForce * clampedPercentage;
        _minJumpForce -= _minJumpForce * clampedPercentage;
        _maxJumpForce -= _maxJumpForce * clampedPercentage;
        _maxHorVelocity -= _maxJumpForce * clampedPercentage;
    }

    private void RestoreMovementPreferences()
    {
        _movementForce = _previousMovementForce;
        _minJumpForce = _previousMinJumpForce;
        _maxJumpForce = _previousMaxJumpForce;
        _maxHorVelocity = _previousMaxHorVelocity;
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
    }

    private void ConfigureFaceDirection()
    {
        if (Math.Abs(_rigidbody2D.velocity.x) > MIN_CHANGE_DIRECTION_SPEED)
        {
            SetDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
        }
    }

    private bool IsJoystickPressed()
    {
        return _joystick.Horizontal != 0 && _joystick.Vertical != 0;
    }
    
    private void HorizontalMovement()
    {
        if (Mathf.Abs(_joystick.Horizontal) > _horizontalSensetivity)
        {
            _rigidbody2D.AddForce(new Vector2(_joystick.Horizontal * _movementForce, 0), _movementMode);
            _rigidbody2D.LimitHorizontalVelocity(_maxHorVelocity);
        }
    }

    private void VerticalMovement()
    {
        if (CanJump())
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(new Vector2(0, Mathf.Clamp(_maxJumpForce * _joystick.Vertical, 
            _minJumpForce, _maxJumpForce)), _movementMode);
        
        _isJumpForbidden = true;
        this.DOWait(_jumpDelay).OnComplete(() => { _isJumpForbidden = false; });

        Messenger.Broadcast(GameEvents.PLAYER_JUMPED);
    }
    
    private bool CanJump()
    {
        if (_isJumpForbidden)
        {
            return false;
        }
        
        return _joystick.Vertical > _verticalSensetivity && _groundChecker.IsGrounded() &&
               LadderMovement.isOnLadder == false;
    }

    private void SetDirection(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);

        movementDirection = direction;
    }
}