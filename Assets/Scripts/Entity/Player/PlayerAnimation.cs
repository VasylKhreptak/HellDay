using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _player;

    [Header("Preferences")]
    [SerializeField] private int _updateFrameRate = 30;
    [SerializeField] private float _sitJoystickSensetivity = -0.7f;
    [SerializeField] private float _punchDelay = 0.6f;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Sit = Animator.StringToHash("Sit");
    private static readonly int LegPunch = Animator.StringToHash("LegPunch");
    private static readonly int ClimbingLadder = Animator.StringToHash("IsClimbingLadder");


    private Coroutine _legPunchActionsCoroutine;

    private void Awake()
    {
        StartCoroutine(ConfigurableUpdate(_updateFrameRate));
    }

    private IEnumerator ConfigurableUpdate(int frameRate)
    {
        float delay = 1 / frameRate;
        
        while (true)
        {
            RunAnimation();

            SitAndUpAnimation();

            LadderClimbing();
            
            yield return new WaitForSeconds(delay);
        }
    }

    private void LadderClimbing()
    {
        _animator.SetBool(ClimbingLadder, LadderMovement.isClimbing);
    }
    
    private void RunAnimation()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void SitAndUpAnimation()
    {
        bool isSitting = _animator.GetBool(Sit);
        
        if (_joystick.Vertical < _sitJoystickSensetivity && isSitting == false)
        {
            if (LadderMovement.isOnLadder == false)
            {
                _animator.SetBool(Sit, true);
                Messenger.Broadcast(GameEvent.PLAYER_SIT_DOWN);
            }
        }
        else if (_joystick.Vertical > _sitJoystickSensetivity && isSitting == true)
        {
            _animator.SetBool(Sit, false);
            Messenger.Broadcast(GameEvent.PLAYER_GET_UP);
        }
    }

    public void StartLegPunch()
    {
        if (_legPunchActionsCoroutine == null && _player.activeSelf == true)
        {
            _legPunchActionsCoroutine = StartCoroutine(LegPunchRoutine());
        }
    }

    private IEnumerator LegPunchRoutine()
    {
        _animator.SetTrigger(LegPunch);

        Messenger<float>.Broadcast(GameEvent.PLAYER_LEG_PUNCH, _punchDelay);

        yield return new WaitForSeconds(_punchDelay);

        _legPunchActionsCoroutine = null;
    }
}