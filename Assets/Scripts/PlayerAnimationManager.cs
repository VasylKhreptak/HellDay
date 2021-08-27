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
            
            Debug.Log("nsgjsd");
        }
        else if(_joystick.Vertical > _sitJoystickSensetivity && _animator.GetBool(Sit))
        {
            _animator.SetBool(Sit, false);
            Messenger.Broadcast(GameEvent.PLAYER_GET_UP);
            Debug.Log("nsgjsd");

        }
    }
}