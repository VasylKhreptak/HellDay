using System.Collections;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;

    [Header("Preferences")] [SerializeField]
    private float _sitJoystickSensetivity = -0.7f;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Sit = Animator.StringToHash("Sit");
    private static readonly int LegPunch = Animator.StringToHash("LegPunch");

    private const float PUNCH_DURATION = 0.6f; //(clip duration)1.8f / (clip speed)2.5f = 0.6f

    private Coroutine _legPunchActionsCoroutine;

    private void Update()
    {
        Run();

        SitAndUp();
    }

    private void Run()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void SitAndUp()
    {
        if (_joystick.Vertical < _sitJoystickSensetivity && _animator.GetBool(Sit) == false)
        {
            _animator.SetBool(Sit, true);
            Messenger.Broadcast(GameEvent.PLAYER_SIT_DOWN);
        }
        else if (_joystick.Vertical > _sitJoystickSensetivity && _animator.GetBool(Sit))
        {
            _animator.SetBool(Sit, false);
            Messenger.Broadcast(GameEvent.PLAYER_GET_UP);
        }
    }

    public void LegPunchActions()
    {
        _legPunchActionsCoroutine ??= StartCoroutine(LegPunchActionsCoroutine());
    }

    private IEnumerator LegPunchActionsCoroutine()
    {
        _animator.SetTrigger(LegPunch);

        Messenger<float>.Broadcast(GameEvent.PLAYER_LEG_PUNCH, PUNCH_DURATION);

        yield return new WaitForSeconds(PUNCH_DURATION);
        
        _legPunchActionsCoroutine = null;
    }
}