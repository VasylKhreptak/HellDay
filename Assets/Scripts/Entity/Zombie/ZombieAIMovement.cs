using System.Collections;
using UnityEngine;

public class ZombieAIMovement : MonoBehaviour
{
    [Header("Target detection preferences")] 
    [SerializeField] protected float _mainDetectionRadius = 5f;
    [SerializeField] protected float _audioDetectionRadius;
    [SerializeField] protected float _increaseDetectionRadiusTime = 10f;

    [Header("References")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] protected KillableTargetDetection _killableTargetDetection;
    
    [Header("Movement preferences")] 
    [SerializeField] protected float _movementSpeed = 3f;
    [SerializeField] protected float _jumpSpeed = 5f;

    [Header("Delays")]
    [Tooltip("Time between possible movement direction change")] 
    [SerializeField] protected float _changeDirectionDelay = 3f;
    [SerializeField] protected float _defaultDelay = 0.5f;
    [SerializeField] protected float _obstacleCheckDelay = 0.3f;

    [Header("Environment checkers")]
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected ObstacleChecker _obstacleChecker;
    [SerializeField] protected BarrierChecker _barrierChecker;

    protected Coroutine _randomMovementCoroutine;
    protected Coroutine _followTargetCoroutine;
    protected Coroutine _increaseDetectionRadiusCoroutine;

    protected bool _isFollowingTarget = false;


    protected void OnEnable()
    {
        Messenger.AddListener(GameEvent.PLAYED_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    protected void Update()
    { 
        _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
    }

    protected void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PLAYED_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    protected void Start()
    {
        StartCoroutine(CheckEnvironmentRoutine());

        StartCoroutine(ControlMovementRoutine());
    }

    protected void OnPlayedAudioSource()
    {
        if (Vector2.Distance(_transform.position,
            _killableTargetDetection.ClosestTarget.Transform.position) < _audioDetectionRadius)
        {
            if (_increaseDetectionRadiusCoroutine == null)
            {
                _increaseDetectionRadiusCoroutine =
                    StartCoroutine(IncreaseDetectionRadiusRoutine(_increaseDetectionRadiusTime));
            }
        }
    }

    protected IEnumerator IncreaseDetectionRadiusRoutine(float time)
    {
        float previousDetectionRadius = _mainDetectionRadius;

        _mainDetectionRadius = _audioDetectionRadius;

        yield return new WaitForSeconds(time);

        _mainDetectionRadius = previousDetectionRadius;

        _increaseDetectionRadiusCoroutine = null;
    }

    protected void StartRandomMovement()
    {
        _isFollowingTarget = false;

        if (_randomMovementCoroutine == null)
        {
            _randomMovementCoroutine = StartCoroutine(RandomMovementRoutine());
        }
    }

    protected void StopRandomMovement()
    {
        if (_randomMovementCoroutine != null)
        {
            StopCoroutine(_randomMovementCoroutine);

            _randomMovementCoroutine = null;
        }
    }

    protected IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 1)
            {
                ReverseMovementDirection();
            }

            yield return new WaitForSeconds(_changeDirectionDelay +
                                            Random.Range(0, _changeDirectionDelay));
        }
    }

    protected void ReverseMovementDirection()
    {
        _movementSpeed *= -1;

        SetFaceDirection((int) Mathf.Sign(_movementSpeed));
    }

    protected void SetMovementDirection(int direction)
    {
        _movementSpeed = Mathf.Sign(direction) * Mathf.Abs(_movementSpeed);
        
        SetFaceDirection(direction);
    }

    protected void SetFaceDirection(int direction)
    {
        _transform.localScale = new Vector3(direction, 1, 1);
    }

    protected IEnumerator CheckEnvironmentRoutine()
    {
        while (true)
        {
            if (CanJump())
            {
                Jump();
            }

            if (CanReverseMovementDirection())
            {
                ReverseMovementDirection();
            }

            yield return new WaitForSeconds(_obstacleCheckDelay);
        }
    }

    protected virtual bool CanJump()
    {
        return _obstacleChecker.isObstacleClose &&
               _groundChecker.IsGrounded();
    }

    protected bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose && _isFollowingTarget == false;
    }

    protected void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpSpeed);
    }

    protected IEnumerator ControlMovementRoutine()
    {
        while (true)
        {
            if (CanFollowTarget())
            {
                StartFollowingTarget(_killableTargetDetection.ClosestTarget.Transform);
                StopRandomMovement();
            }
            else
            {
                StartRandomMovement();
                StopFollowingTarget();
            }

            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    protected bool CanFollowTarget()
    {
        return _transform.ContainsTransform(_mainDetectionRadius,
                   _killableTargetDetection.ClosestTarget.Transform)  &&
               _killableTargetDetection.ClosestTarget.gameObject.activeSelf;
    }
    
    protected void StartFollowingTarget(Transform target)
    {
        _isFollowingTarget = true;

        if (_followTargetCoroutine == null)
        {
            _followTargetCoroutine = StartCoroutine(FollowTargetRoutine(target));
        }
    }

    protected void StopFollowingTarget()
    {
        if (_followTargetCoroutine != null)
        {
            StopCoroutine(_followTargetCoroutine);

            _followTargetCoroutine = null;
        }
    }

    protected IEnumerator FollowTargetRoutine(Transform target)
    {
        while (true)
        {
            LookAtTaregt(target);

            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    protected void LookAtTaregt(Transform target)
    {
        if(target == null) return;
        
        SetMovementDirection(_transform.position.x < target.transform.position.x ? 1 : -1);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_transform.position, _mainDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _audioDetectionRadius);
    }
#endif
}