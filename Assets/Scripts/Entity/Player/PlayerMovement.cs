using System;
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
    [SerializeField] private float _maxHorVelocity = 5f;
    [SerializeField, Range(0f, 1f)] private float _horizontalSensetivity = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _verticalSensetivity = 0.8f;
    [SerializeField] private float _jumpDelay = 1f;
    [SerializeField] private ForceMode2D _movementMode = ForceMode2D.Impulse;
    [SerializeField] private float _minChangeDirectionSpeed = 0.1f;
    [SerializeField] private int _confFaceDirFramerate = 6;
    
    private bool _canMove = true;
    private bool _isJumpForbidden ;
    private bool _isGrounded;
    
    [Range(-1, 1)] private static int movementDirection;
    public static  int Direction => movementDirection;
    

    private float _previousMaxHorVelocity;

    public static Action onJumped;
    private Coroutine _configurableUpdate;

    private void Awake()
    {
        _previousMaxHorVelocity = _maxHorVelocity;
    }

    private void OnEnable()
    {
        PlayerSitAndUpAnimation.onGetUp += () => { _canMove = true; };
        PlayerSitAndUpAnimation.onSitDown += () => { _canMove = false; };
        PlayerWeaponControl.onImpactMovement += ImpactMovement;
        
        SetDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
        
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _confFaceDirFramerate, () =>
        {
            if (IsJoystickPressed())
            {
                ConfigureFaceDirection();
            }
        });
    }

    private void OnDisable()
    {
        PlayerSitAndUpAnimation.onGetUp -= () => { _canMove = true; };
        PlayerSitAndUpAnimation.onSitDown -= () => { _canMove = false;};
        PlayerWeaponControl.onImpactMovement -= ImpactMovement;   
        
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void ImpactMovement(float percentage)
    {
        RestoreMovementPreferences();
        
        float clampedPercentage = percentage / 100f;

        _maxHorVelocity -= _maxHorVelocity * clampedPercentage;
    }

    private void RestoreMovementPreferences()
    {
        _maxHorVelocity = _previousMaxHorVelocity;
    }

    private void Update()
    {
        if (_joystick.Horizontal == 0 || _canMove == false || PlayerLegKickAnimation.IsPlaying) return;

        HorizontalMovement();

        VerticalMovement();
    }

    private void ConfigureFaceDirection()
    {
        if (Math.Abs(_rigidbody2D.velocity.x) > _minChangeDirectionSpeed)
        {
            SetDirection((int) Mathf.Sign(_rigidbody2D.velocity.x));
        }
    }

    private void SetDirection(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);

        movementDirection = direction;
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

        onJumped?.Invoke();
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
}