using DG.Tweening;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    
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

    private void OnEnable()
    {
        Messenger.AddListener(GameEvents.PLAYER_JUMPED, OnPlayerJumped);
        
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFrameRate, () =>
        {
            RunAnimation();
            
            SitAndUpAnimation();

            LadderClimbing();
        });
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvents.PLAYER_JUMPED, OnPlayerJumped);
        
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void OnPlayerJumped()
    {
        _animator.ResetTrigger(Landed);
        _animator.SetTrigger(Jump);

        _playerJumped = true;
        
        Debug.Log("Jumped");
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
                Messenger.Broadcast(GameEvents.PLAYER_SIT_DOWN);
            }
        }
        else if (_joystick.Vertical > _sitJoystickSensetivity && isSitting)
        {
            _animator.SetBool(Sit, false);
            Messenger.Broadcast(GameEvents.PLAYER_GET_UP);
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

        Messenger<float>.Broadcast(GameEvents.PLAYER_LEG_KICK, _legKickDuration);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_playerJumped)
        {
            _animator.ResetTrigger(Jump);
            _animator.SetTrigger(Landed);

            _playerJumped = false;
            
            Debug.Log("Landed");
        }
    }
}