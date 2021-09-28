using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAIMovement : MonoBehaviour
{
    [Header("Target detection sensetivity")] 
    [SerializeField] protected float _mainDetectionRadius = 5f;
    [SerializeField] protected float _audioDetectionRadius;
    [SerializeField] protected float _increaseDetectionRadiusTime = 10f;

    [Header("References")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] protected TargetDetection _targetDetection;
    
    [Header("Movement preferences")] 
    [SerializeField] protected float _movementSpeed = 3f;
    [SerializeField] protected float _jumpVelocity = 5f;

    [Header("Delays")]
    [Tooltip("Time between possible movement direction change")] 
    [SerializeField] protected float _changeDirectionDelay = 3f;
    [SerializeField] protected float _defaultDelay = 0.5f;
    [SerializeField] protected float _obstacleCheckDelay = 0.3f;
    [SerializeField] protected float _findTargetDelay = 1f;

    [Header("Environment checkers")]
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected ObstacleChecker _obstacleChecker;
    [SerializeField] protected BarrierChecker _barrierChecker;

    //Coroutines
    protected Coroutine _randomMovementCoroutine = null;
    protected Coroutine _followTargetCoroutine = null;
    protected Coroutine _increaseDetectionRadiusCoroutine = null;

    protected bool _isFollowingTarget = false;


    protected void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYED_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    protected void Update()
    { 
        _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
    }

    protected void OnDestroy()
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
        if (Vector2.Distance(_transform.position, _targetDetection.closestTarget.position) < _audioDetectionRadius)
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
                                            Random.Range(-_changeDirectionDelay, _changeDirectionDelay));
        }
    }

    protected void ReverseMovementDirection()
    {
        _movementSpeed *= -1;

        SetFaceDirection((int) Mathf.Sign(_movementSpeed));
    }

    protected void SetMovementDirection(int direction)
    {
        if (direction > 0)
        {
            _movementSpeed = Mathf.Abs(_movementSpeed);
        }
        else
        {
            _movementSpeed = -Mathf.Abs(_movementSpeed);
        }

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
            if (CanJump() == true)
            {
                Jump();
            }

            if (CanReverseMovementDirection() == true)
            {
                ReverseMovementDirection();
            }

            yield return new WaitForSeconds(_obstacleCheckDelay);
        }
    }

    protected virtual bool CanJump()
    {
        return _obstacleChecker.isObstacleClose == true &&
               _groundChecker.isGrounded == true;
    }

    protected bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose == true && _isFollowingTarget == false;
    }

    protected void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }

    protected IEnumerator ControlMovementRoutine()
    {
        while (true)
        {
            if (_transform.ContainsTransform(_mainDetectionRadius, _targetDetection.closestTarget) == true)
            {
                StartFollowingTarget(_targetDetection.closestTarget);
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
            LookToTarget(target);

            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    protected void LookToTarget(Transform target)
    {
        if (_transform.position.x < target.position.x)
        {
            SetMovementDirection(1);
        }
        else
        {
            SetMovementDirection(-1);
        }
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
        //Detection
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_transform.position, _mainDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _audioDetectionRadius);

        //ClosestTarget
        Gizmos.color = Color.red;
        if (_targetDetection.closestTarget != null)
        {
            Gizmos.DrawWireCube(_targetDetection.closestTarget.position, Vector2.one);

            if (_transform.ContainsTransform(_audioDetectionRadius, _targetDetection.closestTarget))
            {
                Gizmos.DrawLine(_transform.position, _targetDetection.closestTarget.position);
            }
        }
    }
#endif
}