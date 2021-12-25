using System;
using UnityEngine;

public class PlayerSitAndUpAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;

    [Header("Preferences")]
    [SerializeField] private float _sitJoystickSensetivity = -0.7f;
    [SerializeField] private int _updateFramerate = 6;

    public static Action onSitDown;
    public static Action onGetUp;

    private Coroutine _configurableUpdate;

    private readonly int Sit = Animator.StringToHash("Sit");

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate, () => {
            ControlSitAndUpAnimtion();
        });
    }

    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void ControlSitAndUpAnimtion()
    {
        var isSitting = _animator.GetBool(Sit);

        if (_joystick.Vertical < _sitJoystickSensetivity && isSitting == false)
        {
            if (LadderMovement.isOnLadder == false)
            {
                _animator.SetBool(Sit, true);
                onSitDown?.Invoke();
            }
        }
        else if (_joystick.Vertical > _sitJoystickSensetivity && isSitting)
        {
            _animator.SetBool(Sit, false);
            onGetUp?.Invoke();
        }
    }
}
