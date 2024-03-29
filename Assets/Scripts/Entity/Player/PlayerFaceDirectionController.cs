using System;
using UnityEngine;

public class PlayerFaceDirectionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Joystick _joystick;

    [Header("Preferences")]
    [SerializeField] private float _updateFramerate = 2f;
    [SerializeField] [Range(0f, 1f)] private float _joystickSensetivity = 0.3f;

    private Coroutine _configurableUpdate;

    [Range(-1, 1)] private static int _faceDirection;

    public static int FaceDirection => _faceDirection;

    private void OnEnable()
    {
        StartConfiguringFaceDirection();
    }

    private void OnDisable()
    {
        StopConfiguringFaceDirection();
    }

    private void Awake()
    {
        SetDirection((int)Mathf.Round(_transform.localScale.x));
    }

    private void StartConfiguringFaceDirection()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate, () =>
        {
            if (CanConfigureFaceDirection()) SetDirection((int)Mathf.Sign(_joystick.Horizontal));
        });
    }

    private void StopConfiguringFaceDirection()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void SetDirection(int direction)
    {
        _transform.localScale = new Vector3(direction, 1, 1);

        _faceDirection = direction;
    }

    private bool CanConfigureFaceDirection()
    {
        return Mathf.Approximately(_joystick.Horizontal, 0) == false &&
               Mathf.Abs(_joystick.Horizontal) > _joystickSensetivity;
    }
}