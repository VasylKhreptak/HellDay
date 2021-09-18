using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAIMovement : MonoBehaviour
{
    [Header("Target")] [SerializeField] private Transform _target;

    [Header("Target detection sensetivity")] [SerializeField]
    private float _mainDetectionRadius = 5f;

    [SerializeField] private float _audioDetectionRadius;

    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [Header("Movement preferences")] [SerializeField]
    private float _movementSpeed = 3f;

    [SerializeField] private float _jumpVelocity = 5f;

    [Header("Delays")] [Tooltip("Time between possible movement direction change")] [SerializeField]
    private float _changeDirectionDelay = 3f;

    [SerializeField] private float _defaultDelay = 0.5f;
    [SerializeField] private float _obstacleCheckDelay = 0.3f;

    [Header("Environment checkers")] [SerializeField]
    private GroundChecker _groundChecker;

    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private BarrierChecker _barrierChecker;

    //Coroutines
    private Coroutine _randomMovementCoroutine = null;
    private Coroutine _followTargetCoroutine = null;

    private bool _isFollowingTarget = false;

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
    }

    private void Start()
    {
        StartCoroutine(CheckEnvironment());

        StartCoroutine(ControlMovementRoutine());
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
            if (IsTargetClose(_mainDetectionRadius) == true)
            {
                StartFollowingTarget();
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

    private bool IsTargetClose(float detectionRadius)
    {
        return Vector2.Distance(transform.position, _target.position) < detectionRadius;
    }

    private void StartFollowingTarget()
    {
        _isFollowingTarget = true;

        if (_followTargetCoroutine == null)
        {
            _followTargetCoroutine = StartCoroutine(FollowingTargetRoutine());
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

    private IEnumerator FollowingTargetRoutine()
    {
        while (true)
        {
            LookToTarget();

            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    private void LookToTarget()
    {
        if (transform.position.x < _target.position.x)
        {
            SetMovementDirection(1);
        }
        else
        {
            SetMovementDirection(-1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _mainDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _audioDetectionRadius);
    }
}