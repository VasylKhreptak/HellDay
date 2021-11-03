using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanAIMovement : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("MovementPreferences")] 
    [SerializeField] private float _movementSpeed = 13f;
    [SerializeField] private float _jumpSpeed = 18f;

    [Header("Delays")]
    [SerializeField] private float _obstacleCheckDelay;
    [SerializeField] private float _defaultDelay = 0.5f;
    [SerializeField] private float _chagneDirectionDelay = 10f;
    
    [Header("Environment checkers")] 
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private ObstacleChecker _obstacleChecker;
    [SerializeField] private BarrierChecker _barrierChecker;
    [SerializeField] private PitChecker _pitChecker;

    [Header("Threat detection preferences")]
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private KillableTargetDetection _killableTargetDetection;
    
    private bool _canMove = true;
    private bool _isRunningFromThreat;

    private Coroutine _randomMovementCoroutine;
    private Coroutine _runFromThreatCoroutine;
    private Coroutine _checkEnvironmentCorouitne;

    private void Update()
    {
        if (_canMove)
        {
            _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
        }
    }

    private void Start()
    {
        StartCheckingEnvironment();
        
        StartCoroutine(ControlMovementRoutine());
    }

    private void StartCheckingEnvironment()
    {
        if (_checkEnvironmentCorouitne == null)
        {
            _checkEnvironmentCorouitne = StartCoroutine(CheckEnvironmentRoutine());
        }
    }

    private void StopCheckingEnvironment()
    {
        if (_checkEnvironmentCorouitne != null)
        {
            StopCoroutine(_checkEnvironmentCorouitne);

            _checkEnvironmentCorouitne = null;
        }
    }
    
    private IEnumerator CheckEnvironmentRoutine()
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
    
    private IEnumerator ControlMovementRoutine()
    {
        while (true)
        {
            if (IsThreatClose())
            {
                StopRandomMovement();
                StartRunningFromThreat();
            }
            else
            {
                StartRandomMovement();
                StopRunningFromThreat();
            }
            
            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    private bool CanJump()
    {
        return (_obstacleChecker.isObstacleClose  || _pitChecker.isPitNearp) &&
               _groundChecker.IsGrounded();
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpSpeed);
    }

    private bool CanReverseMovementDirection()
    {
        if (_isRunningFromThreat) return false;
        
        return _barrierChecker.isBarrierClose;
    }

    private void ReverseMovementDirection()
    {
        _movementSpeed *= -1;
        
        SetFaceDirection((int) Mathf.Sign(_movementSpeed));
    }

    private void SetFaceDirection(int direction)
    {
        _transform.localScale = new Vector3(direction, 1, 1);
    }

    private void StartRandomMovement()
    {
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

            StopStaying();
            
            _randomMovementCoroutine = null;
        }
    }

    private IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 1)
            {
                StopStaying();
                ReverseMovementDirection();
            }
            else
            {
                StartStaying();
            }

            yield return new WaitForSeconds(_chagneDirectionDelay +
                                            Random.Range(0, _chagneDirectionDelay));
        }
    }

    private bool IsThreatClose()
    {
        return _transform.ContainsTransform(_detectionRadius, _killableTargetDetection.ClosestTarget.Transform);
    }

    private void StartRunningFromThreat()
    {
        if (_runFromThreatCoroutine == null)
        {
            _runFromThreatCoroutine = StartCoroutine(RunFromThreatRoutine());

            _isRunningFromThreat = true;
        }
    }

    private IEnumerator RunFromThreatRoutine()
    {
        while (true)
        {
            TurnAwayFromThreat();
            
            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    private void TurnAwayFromThreat()
    {
        Transform target = _killableTargetDetection.ClosestTarget.Transform;
        
        SetMovementDirection(_transform.position.x < target.transform.position.x ? -1 : 1);
    }

    private void SetMovementDirection(int direction)
    {
        _movementSpeed = Mathf.Sign(direction) * Mathf.Abs(_movementSpeed);

        SetFaceDirection(direction);
    }

    private void StartStaying()
    {
        _canMove = false;
        
        StopCheckingEnvironment();
    }

    private void StopStaying()
    {
        _canMove = true;
        
        StartCheckingEnvironment();
    }

    private void StopRunningFromThreat()
    {
        if (_runFromThreatCoroutine != null)
        {
            StopCoroutine(_runFromThreatCoroutine);

            _runFromThreatCoroutine = null;

            _isRunningFromThreat = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_transform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _detectionRadius);

        if (_killableTargetDetection.ClosestTarget == null) return;
        
        Transform threat = _killableTargetDetection.ClosestTarget.Transform;
        
        Gizmos.DrawLine(_transform.position, threat.position);
        Gizmos.DrawCube(threat.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
#endif
}
