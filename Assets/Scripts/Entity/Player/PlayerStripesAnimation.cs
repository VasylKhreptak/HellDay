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
    [SerializeField] private float _updateFramerate = 10;

    [Header("Animation Preferences")]
    [SerializeField] private Transform _upTransform;
    [SerializeField] private Transform _middleTransform;
    [SerializeField] private Transform _bottomTransform;
    [SerializeField] private float _rotationDur;

    [Header("Player Sit Down Preferences")]
    [SerializeField] private float _minYOffset = 1f;
    [SerializeField] private float _moveDur = 1f;

    [Header("Preferences")]
    [SerializeField] private float _accuracy = 0.3f;

    private float _startYpos;
    private float _targetYpos;
    private void Awake()
    {
        _startYpos = _transform.localPosition.y;
        _targetYpos = _transform.localPosition.y - _minYOffset;
    }

    private readonly int Speed = Animator.StringToHash("Speed");

    private Coroutine _configurableUpdate;

    private Tween _rotateTween;
    private Tween _moveTween;

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate, () =>
        {
            _animator.SetFloat(Speed, _playerRb.velocity.magnitude);

            AlignStripes();
        });

        PlayerSitAndUpAnimation.onSitDown += MoveDown;
        PlayerSitAndUpAnimation.onGetUp += MoveUp;
    }

    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);

        _rotateTween.Kill();
        _moveTween.Kill();

        PlayerSitAndUpAnimation.onSitDown -= MoveDown;
        PlayerSitAndUpAnimation.onGetUp -= MoveUp;
    }

    private void MoveDown()
    {
        Move(false);
    }

    private void MoveUp()
    {
        Move(true);
    }

    private void Move(bool up)
    {
        _moveTween.Kill();
        _moveTween = _transform.DOLocalMoveY(up ? _startYpos : _targetYpos, _moveDur);
    }

    private void AlignStripes()
    {
        if (IsMovingVertical(out var up)) LookAt(up ? _bottomTransform : _upTransform);
        else if (IsMovingHorizontal()) LookAt(_middleTransform);
        else LookAt(_bottomTransform);
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
        var direction = target.position - _transform.position;

        if (PlayerFaceDirectionController.FaceDirection == 1)
        {
            direction = Quaternion.AngleAxis(180, Vector3.forward) * direction;
        }

        var rot = Extensions.Quaternion.RotationFromDirection2D(direction);

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
