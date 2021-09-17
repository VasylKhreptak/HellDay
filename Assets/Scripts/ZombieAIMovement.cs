using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAIMovement : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [Header("Animator")] [SerializeField] private Animator _animator;
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    [Header("Movement preferences")] [SerializeField]
    private float _movementSpeed = 3f;

    [SerializeField] private float _jumpVelocity = 5f;

    [Tooltip("Time between possible movement direction change")] [SerializeField]
    private float _changeDirectionDelay = 3f;

    [Header("GroundChecker")] [SerializeField]
    private GroundChecker _groundChecker;

    [SerializeField] private ObstacleChecker _obstacleChecker;

    [SerializeField] private float _obstacleCheckDelay = 0.5f;

    private bool _canMove = true;
    private Coroutine _movementCoroutine = null;

    private void Update()
    {
        if (_canMove == true)
        {
            _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
        }
    }

    private void Start()
    {
        StartRandomMovement();

        StartCoroutine(CheckObstacleCoroutine());
    }

    private void StartRandomMovement()
    {
        _canMove = true;

        SetMovementAnimation(true);

        if (_movementCoroutine == null)
            _movementCoroutine = StartCoroutine(StartRandomMovementCoroutine());
    }

    private void StopRandomMovement()
    {
        _canMove = false;

        SetMovementAnimation(false);

        StopCoroutine(_movementCoroutine);
    }

    private IEnumerator StartRandomMovementCoroutine()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 1)
            {
                ChangeMovementDirection();
            }

            yield return new WaitForSeconds(_changeDirectionDelay);
        }
    }

    private void ChangeMovementDirection()
    {
        _movementSpeed *= -1;

        ChangeFaceDirection();
    }

    private void ChangeFaceDirection()
    {
        transform.localScale = new Vector3(Mathf.Sign(_movementSpeed), 1, 1);
    }

    private void SetMovementAnimation(bool state)
    {
        _animator.SetBool(IsMoving, state);
    }

    private IEnumerator CheckObstacleCoroutine()
    {
        while (true)
        {
            if (_obstacleChecker.isObstacleClose == true)
            {
                if (_groundChecker.isGrounded == true)
                {
                    Jump();
                }
            }

            yield return new WaitForSeconds(_obstacleCheckDelay);
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
    }
}