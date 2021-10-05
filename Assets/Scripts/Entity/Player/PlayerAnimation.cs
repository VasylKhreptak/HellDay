using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _player;
    [SerializeField] private int _updateFrameRate = 30;
    [SerializeField] private CoroutineObject _configurableUpdate;

    [Header("Preferences")] [SerializeField]
    private float _sitJoystickSensetivity = -0.7f;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Sit = Animator.StringToHash("Sit");
    private static readonly int LegPunch = Animator.StringToHash("LegPunch");

    private const float PUNCH_DURATION = 0.6f; //(clip duration)1.8f / (clip speed)2.5f = 0.6f

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
            
            yield return new WaitForSeconds(delay);
        }
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
            _animator.SetBool(Sit, true);
            Messenger.Broadcast(GameEvent.PLAYER_SIT_DOWN);
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

        Messenger<float>.Broadcast(GameEvent.PLAYER_LEG_PUNCH, PUNCH_DURATION);

        yield return new WaitForSeconds(PUNCH_DURATION);

        _legPunchActionsCoroutine = null;
    }
}