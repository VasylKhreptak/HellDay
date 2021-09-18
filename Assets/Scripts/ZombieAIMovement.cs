using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAIMovement : MonoBehaviour
{
    [Header("Targets")] [SerializeField] private Transform[] _targets;

    [Header("Target detection sensetivity")] [SerializeField]
    private float _mainDetectionRadius = 5f;

    [SerializeField] private float _audioDetectionRadius;
    [SerializeField] private float _increaseDetectionRadiusTime = 10f;

    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [Header("Movement preferences")] [SerializeField]
    private float _movementSpeed = 3f;

    [SerializeField] private float _jumpVelocity = 5f;

    [Header("Delays")] [Tooltip("Time between possible movement direction change")] [SerializeField]
    private float _changeDirectionDelay = 3f;

    [SerializeField] private float _defaultDelay = 0.5f;
    [SerializeField] private float _obstacleCheckDelay = 0.3f;
    [SerializeField] private float _findTargetDelay = 1f;

    [Header("Environment checkers")] [SerializeField]
    private GroundChecker _groundChecker;

    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private BarrierChecker _barrierChecker;

    //Coroutines
    private Coroutine _randomMovementCoroutine = null;
    private Coroutine _followTargetCoroutine = null;
    private Coroutine _increaseDetectionRadiusCoroutine = null;

    private bool _isFollowingTarget = false;

    private Transform _closestTarget;

    private void Awake()
    {
        StartCoroutine(FindClosestTargetRoutine());

        Messenger.AddListener(GameEvent.PLAYED_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYED_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    private void Start()
    {
        StartCoroutine(CheckEnvironment());

        StartCoroutine(ControlMovementRoutine());
    }

    private void OnPlayedAudioSource()
    {
        if (Vector2.Distance(transform.position, _closestTarget.position) < _audioDetectionRadius)
        {
            if (_increaseDetectionRadiusCoroutine == null)
            {
                _increaseDetectionRadiusCoroutine =
                    StartCoroutine(IncreaseDetectionRadiusRoutine(_increaseDetectionRadiusTime));
            }
        }
    }

    private IEnumerator IncreaseDetectionRadiusRoutine(float time)
    {
        float previousDetectionRadius = _mainDetectionRadius;

        _mainDetectionRadius = _audioDetectionRadius;

        yield return new WaitForSeconds(time);

        _mainDetectionRadius = previousDetectionRadius;

        _increaseDetectionRadiusCoroutine = null;
    }

    private void StartRandomMovement()
    {
        _isFollowingTarget = false;

        if (_randomMovementCoroutine == null)
        {
            _randomMovementCoroutine = StartCoroutine(RandomMovementRoutine());
        }
    }

    private void StopRandomMovement()
    {
        if (_randomMovementCoroutine != null)
        {
            StopCoroutine(_randomMovementCoroutine);

            _randomMovementCoroutine = null;
        }
    }

    private IEnumerator RandomMovementRoutine()
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

    private void ReverseMovementDirection()
    {
        _movementSpeed *= -1;

        SetFaceDirection((int) Mathf.Sign(_movementSpeed));
    }

    private void SetMovementDirection(int direction)
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

    private void SetFaceDirection(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private IEnumerator CheckEnvironment()
    {
        while (true)
        {
            if (_obstacleChecker.isObstacleClose == true &&
                _groundChecker.isGrounded == true)
            {
                Jump();
            }

            if (_barrierChecker.isBarrierClose == true && _isFollowingTarget == false)
            {
                ReverseMovementDirection();
            }

            yield return new WaitForSeconds(_obstacleCheckDelay);
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }

    private IEnumerator ControlMovementRoutine()
    {
        while (true)
        {
            if (IsTargetClose(_mainDetectionRadius, _closestTarget) == true)
            {
                StartFollowingTarget(_closestTarget);
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

    private bool IsTargetClose(float detectionRadius, Transform target)
    {
        return Vector2.Distance(transform.position, target.position) < detectionRadius;
    }

    private void StartFollowingTarget(Transform target)
    {
        _isFollowingTarget = true;

        if (_followTargetCoroutine == null)
        {
            _followTargetCoroutine = StartCoroutine(FollowTargetRoutine(target));
        }
    }

    private void StopFollowingTarget()
    {
        if (_followTargetCoroutine != null)
        {
            StopCoroutine(_followTargetCoroutine);

            _followTargetCoroutine = null;
        }
    }

    private IEnumerator FollowTargetRoutine(Transform target)
    {
        while (true)
        {
            LookToTarget(target);

            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    private void LookToTarget(Transform target)
    {
        if (transform.position.x < target.position.x)
        {
            SetMovementDirection(1);
        }
        else
        {
            SetMovementDirection(-1);
        }
    }

    private IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            _closestTarget = FindClosestTarget(_targets);

            yield return new WaitForSeconds(_findTargetDelay);
        }
    }

    private Transform FindClosestTarget(Transform[] targets)
    {
        Transform closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform potentialTarget in targets)
        {
            Vector3 directionToTarget = potentialTarget.position - transform.position;
            float sqrDirectionToTarget = directionToTarget.sqrMagnitude;

            if (sqrDirectionToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDirectionToTarget;
                closestTarget = potentialTarget;
            }
        }

        return closestTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Detection
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _mainDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _audioDetectionRadius);

        //ClosestTarget
        Gizmos.color = Color.red;
        if (_closestTarget != null)
            Gizmos.DrawWireCube(_closestTarget.position, Vector2.one);
    }
#endif
}