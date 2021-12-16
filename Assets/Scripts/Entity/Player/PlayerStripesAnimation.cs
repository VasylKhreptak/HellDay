using DG.Tweening;
 using UnityEngine;

public class PlayerStripesAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private Transform _transform; 
    [SerializeField] private Animator _animator;

    [Header("Update Preferences")]
    [SerializeField] private int _updateFramerate  = 10;

    [Header("Animation Preferences")]
    [SerializeField] private Transform _upTransform;
    [SerializeField] private Transform _middleTransform;
    [SerializeField] private Transform _bottomTransform;
    [SerializeField] private float _rotationDur;
    
    private readonly int Speed = Animator.StringToHash("Speed");
    
    private Coroutine _configurableUpdate;

    private Tween _rotateTween;

    private void OnEnable()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFramerate, () =>
            {
                _animator.SetFloat(Speed, _playerRb.velocity.magnitude);

                float vertVelocity = _playerRb.velocity.y;

                if (Mathf.Approximately(vertVelocity, 0) == false)
                {
                    LookAt(Mathf.Sign(vertVelocity) == 1 ? _bottomTransform : _upTransform);
                }
            });
    }

    private void OnDisable()
    {
        ConfigurableUpdate.StopUpdate(this, ref _configurableUpdate);
    }

    private void LookAt(Transform target)
    {
        _transform.DOLookAt(target.position, _rotationDur, AxisConstraint.X | AxisConstraint.Y);
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _upTransform == null || _middleTransform == null || _bottomTransform == null) 
            return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_transform.position, _upTransform.position);
        Gizmos.DrawLine(_transform.position, _middleTransform.position);
        Gizmos.DrawLine(_transform.position, _bottomTransform.position);
    }
}
