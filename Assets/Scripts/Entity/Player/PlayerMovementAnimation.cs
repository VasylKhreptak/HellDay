using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Joystick _joystick;

    [Header("Preferences")]
    [SerializeField] private int _updateFramerate = 6;

    private Coroutine _configurableUpdate;

    private static bool _isMoving;
    public static bool IsMoving => _isMoving;

    private readonly int Speed = Animator.StringToHash("Speed");

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate, () => 
        {
            ControlMovementAnimation();

            _isMoving = Mathf.Approximately(_rigidbody2D.velocity.x, 0) == false;
        });
    }

    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void ControlMovementAnimation()
    {
        _animator.SetFloat(Speed, _joystick.IsPressed() ? Mathf.Abs(_rigidbody2D.velocity.x) : 0);
    }
}
