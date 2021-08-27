using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;

    [Header("Preferences")] [SerializeField]
    private float _sitSensetivity = -0.8f;


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
        _animator.SetBool(Sit, _joystick.Vertical < _sitSensetivity);
    }
}