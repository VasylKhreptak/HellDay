using System;
using DG.Tweening;
using UnityEngine;

public class PlayerStripesAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private Transform _transform; 
    [SerializeField] private Animator _animator;

    [Header("Update Preferences")]
    [SerializeField] private float _updateFramerate  = 10;

    [Header("Animation Preferences")]
    [SerializeField] private Transform _upTransform;
    [SerializeField] private Transform _middleTransform;
    [SerializeField] private Transform _bottomTransform;
    [SerializeField] private float _rotationDur;

    [Header("Preferences")] 
    [SerializeField] private float _accuracy = 0.3f;
    
    private readonly int Speed = Animator.StringToHash("Speed");
    
    private Coroutine _configurableUpdate;

    private Tween _rotateTween;
    private Tween _moveTween;
    
    private Vector3 _previousPos;

    private void Awake()
    {
        _previousPos = _transform.position;
    }

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate, () =>
        {
            _animator.SetFloat(Speed, _playerRb.velocity.magnitude);

            AlignStripes();
        });
    }
    
    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);

        _rotateTween.Kill();
        _moveTween.Kill();
    }

    private void AlignStripes()
    {
        if (IsMovingVertical(out bool up))
        {
            LookAt(up ? _bottomTransform : _upTransform);
        }
        else if (IsMovingHorizontal())
        {
            LookAt(_middleTransform);
        }
        else
        {
            LookAt(_bottomTransform);
        }
    }

    private bool IsMovingVertical(out bool movingUp)
    {
        movingUp = Mathf.Sign(_playerRb.velocity.y) == 1;

        return Extensions.Mathf.Approximately(_playerRb.velocity.y, 0, _accuracy) == false;
    }

    private bool IsMovingHorizontal()
    {
        return Extensions.Mathf.Approximately(_playerRb.velocity.x, 0, _accuracy) == false;
    }

    private void LookAt(Transform target)
    {
        Vector3 direction = target.position - _transform.position;

        if (PlayerMovement.Direction == 1)
        {
            direction = Quaternion.AngleAxis(180, Vector3.forward) * direction;
        }
        
        Quaternion rot = Extensions.Quaternion.RotationFromDirection2D(direction);
        
        _rotateTween.Kill();
        _rotateTween = _transform.DORotate(rot.eulerAngles, _rotationDur);
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _upTransform == null || 
            _middleTransform == null || _bottomTransform == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_transform.position, _upTransform.position);
        Gizmos.DrawLine(_transform.position, _middleTransform.position);
        Gizmos.DrawLine(_transform.position, _bottomTransform.position);
    }
}
