using System;
using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private WalkAudio _playerWalkAudio;
    
    [Header("Preferences")]
    [SerializeField] private int _updateFrameRate = 30;
    [SerializeField] private float _sitJoystickSensetivity = -0.7f;

    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int Sit = Animator.StringToHash("Sit");
    private readonly int LegPunch = Animator.StringToHash("LegPunch");
    private readonly int ClimbingLadder = Animator.StringToHash("IsClimbingLadder");
    private readonly int Jump = Animator.StringToHash("Jump");
    private readonly int Landed = Animator.StringToHash("Landed");

    private bool _playerJumped;
    private float _legKickDuration;
    
    private Coroutine _configurableUpdate;

    public static Action onPlayerGetUp;
    public static Action onPlayerSitDown;
    public static Action<float> onPlayerLegKick;

    private void OnEnable()
    {
        PlayerMovement.onPlayerJumped += OnPlayerJumped;
        
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFrameRate, () =>
        {
            RunAnimation();
            
            SitAndUpAnimation();

            LadderClimbing();
        });
    }

    private void OnDisable()
    {
        PlayerMovement.onPlayerJumped -= OnPlayerJumped;

        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void OnPlayerJumped()
    {
        _animator.ResetTrigger(Landed);
        _animator.SetTrigger(Jump);

        _playerJumped = true;
    }

    private void RunAnimation()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void LadderClimbing()
    {
        _animator.SetBool(ClimbingLadder, LadderMovement.isClimbing);
    }
    
    private void SitAndUpAnimation()
    {
        bool isSitting = _animator.GetBool(Sit);
        
        if (_joystick.Vertical < _sitJoystickSensetivity && isSitting == false)
        {
            if (LadderMovement.isOnLadder == false)
            {
                _animator.SetBool(Sit, true);
                onPlayerSitDown?.Invoke();
            }
        }
        else if (_joystick.Vertical > _sitJoystickSensetivity && isSitting)
        {
            _animator.SetBool(Sit, false);
            onPlayerGetUp?.Invoke();
        }
    }

    public void PlayLegKickAnimation()
    {
        _animator.SetTrigger(LegPunch);

        this.DOWait(0.1f).OnComplete(() =>
        {
            _animator.ResetTrigger(LegPunch);
        });

        if (_legKickDuration == 0)
        {
            _legKickDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
        }
        
        onPlayerLegKick?.Invoke(_legKickDuration);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_playerJumped)
        {
            _playerWalkAudio.PlayStepSound();
            
            _animator.ResetTrigger(Jump);
            _animator.SetTrigger(Landed);

            _playerJumped = false;
        }
    }
}