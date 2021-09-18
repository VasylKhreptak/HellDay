using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAIMovement : MonoBehaviour
{
    [Header("Target")] [SerializeField] private Transform _target;

    [Header("Target detection sensetivity")] [SerializeField]
    private float _smallDetectionRadius = 5f;

    [SerializeField] private float _mediumDetectionRadius = 15f;
    [SerializeField] private float _audioDetectionRadius;

    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    [Header("Movement preferences")] [SerializeField]
    private float _movementSpeed = 3f;

    [SerializeField] private float _jumpVelocity = 5f;


    [Header("Delays")] [Tooltip("Time between possible movement direction change")] [SerializeField]
    private float _changeDirectionDelay = 3f;

    [Tooltip("Time between possible movement state change")] [SerializeField]
    private float _changeMovementStateDelay = 5;

    [SerializeField] private float _controlGeneralMovementDelay = 1f;
    [SerializeField] private float _standartDelay = 0.5f;

    [Header("GroundChecker")] [SerializeField]
    private GroundChecker _groundChecker;

    [SerializeField] private ObstacleChecker _obstacleChecker;
    
    private bool _canMove = true;

    //Coroutines
    private Coroutine _randomMovementCoroutine = null;
    private Coroutine _controlRandomMovementCoroutine = null;
    private Coroutine _followTargetCoroutine = null;

    private void Update()
    {
        if (_canMove == true)
        {
            _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
        }
    }

    private void Start()
    {
        StartCoroutine(CheckObstacle());

        StartCoroutine(ControlGeneralMovement());
    }

    private void StartRandomMovement()
    {
        _canMove = true;

        SetMovementAnimation(true);

        _randomMovementCoroutine = StartCoroutine(StartRandomMovementRoutine());
    }

    private void StopRandomMovement()
    {
        _canMove = false;

        SetMovementAnimation(false);

        if (_randomMovementCoroutine != null)
            StopCoroutine(_randomMovementCoroutine);
    }

    private IEnumerator StartRandomMovementRoutine()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 1 && 
                _canMove == true)
            {
                ReverseMovementDirection();
            }

            yield return new WaitForSeconds(_changeDirectionDelay);
        }
    }

    private void ReverseMovementDirection()
    {
        _movementSpeed *= -1;

        SetFaceDirection((int) Mathf.Sign(_movementSpeed));
    }

    private void SetMovementDirection(int direction)
    {
        _movementSpeed *= direction;

        SetFaceDirection(direction);
    }

    private void SetFaceDirection(int direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void SetMovementAnimation(bool state)
    {
        _animator.SetBool(IsMoving, state);
    }

    private IEnumerator CheckObstacle()
    {
        while (true)
        {
            if (_obstacleChecker.isObstacleClose == true &&
                _groundChecker.isGrounded == true && 
                _canMove == true)
            {
                Jump();
            }

            yield return new WaitForSeconds(_standartDelay +
                                            Random.Range(0, _standartDelay / 2f));
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }

    private IEnumerator ControlRandomMovement()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 1)
            {
                StartRandomMovement();
            }
            else
            {
                StopRandomMovement();
            }

            yield return new WaitForSeconds(_changeMovementStateDelay);
        }
    }

    private void StopControlRandomMovement()
    {
        if (_controlRandomMovementCoroutine != null)
        {
            StopCoroutine(_controlRandomMovementCoroutine);
        }
    }

    private IEnumerator ControlGeneralMovement()
    {
        while (true)
        {
            if (IsZombieClose(_smallDetectionRadius) == true)
            {
                StopControlRandomMovement();

                _followTargetCoroutine = StartCoroutine(FollowTaregtRoutine());
            }
            else
            {
                _controlRandomMovementCoroutine = StartCoroutine(ControlRandomMovement());

                if (_followTargetCoroutine != null)
                {
                    StopCoroutine(_followTargetCoroutine);
                }
            }

            yield return new WaitForSeconds(_controlGeneralMovementDelay);
        }
    }

    private bool IsZombieClose(float detectionRadius)
    {
        return Vector2.Distance(transform.position, _target.position) < detectionRadius;
    }

    private IEnumerator FollowTaregtRoutine()
    {
        while (true)
        {
            if (transform.position.x < _target.position.x)
            {
                SetMovementDirection(1);
            }
            else
            {
                SetMovementDirection(-1);
            }

            yield return new WaitForSeconds(_standartDelay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _smallDetectionRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _mediumDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _audioDetectionRadius);
    }
}