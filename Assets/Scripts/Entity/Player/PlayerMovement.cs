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
    [SerializeField] [Range(0f, 1f)] private float _horizontalSensetivity = 0.5f;
    [SerializeField] [Range(0f, 1f)] private float _verticalSensetivity = 0.8f;
    [SerializeField] private float _jumpDelay = 1f;
    [SerializeField] private ForceMode2D _movementMode = ForceMode2D.Impulse;

    private bool _canMove = true;
    private bool _isJumpForbidden;
    private bool _isGrounded;

    private float _previousMaxHorVelocity;

    public static Action onJumped;

    private void Awake()
    {
        _previousMaxHorVelocity = _maxHorVelocity;
    }

    private void OnEnable()
    {
        PlayerSitAndUpAnimation.onGetUp += () => { _canMove = true; };
        PlayerSitAndUpAnimation.onSitDown += () => { _canMove = false; };
        PlayerWeaponControl.onImpactMovement += ImpactMovement;
    }

    private void OnDisable()
    {
        PlayerSitAndUpAnimation.onGetUp -= () => { _canMove = true; };
        PlayerSitAndUpAnimation.onSitDown -= () => { _canMove = false; };
        PlayerWeaponControl.onImpactMovement -= ImpactMovement;
    }

    private void ImpactMovement(float percentage)
    {
        RestoreMovementPreferences();

        var clampedPercentage = percentage / 100f;

        _maxHorVelocity -= _maxHorVelocity * clampedPercentage;
    }

    private void RestoreMovementPreferences()
    {
        _maxHorVelocity = _previousMaxHorVelocity;
    }

    private void FixedUpdate()
    {
        if (_joystick.Horizontal == 0 || _canMove == false || PlayerLegKickAnimation.IsPlaying) return;

        HorizontalMovement();

        VerticalMovement();
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
        if (CanJump()) Jump();
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
        if (_isJumpForbidden) return false;

        return _joystick.Vertical > _verticalSensetivity && _groundChecker.IsGrounded() &&
               LadderMovement.isOnLadder == false;
    }
}