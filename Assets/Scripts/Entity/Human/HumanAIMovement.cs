using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanAIMovement : AIMovementCore
{
    [Header("Environment checkers")]
    [SerializeField] protected PitChecker _pitChecker;

    [Header("Threat detection preferences")]
    [SerializeField] protected float _detectionRadius = 5f;
    [SerializeField] protected KillableTargetDetection _killableTargetDetection;

    [Header("Sign Scale Compensate")] 
    [SerializeField] private SignScaleCompensate _sign;
    
    protected bool _canMove = true;
    protected bool _isRunningFromThreat;

    protected Coroutine _runFromThreatCoroutine;

    protected override void Update()
    {
        if (_canMove)
        {
            _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
        }
    }

    protected void Start()
    {
        StartCheckingEnvironment();
        
        StartCoroutine(ControlMovementRoutine());
    }

    protected void StartCheckingEnvironment()
    {
        if (_checkEnvironmentCorouitne == null)
        {
            _checkEnvironmentCorouitne = StartCoroutine(CheckEnvironmentRoutine());
        }
    }

    protected void StopCheckingEnvironment()
    {
        if (_checkEnvironmentCorouitne != null)
        {
            StopCoroutine(_checkEnvironmentCorouitne);

            _checkEnvironmentCorouitne = null;
        }
    }

    protected IEnumerator ControlMovementRoutine()
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

    protected override bool CanJump()
    {
        return (_obstacleChecker.isObstacleClose  || _pitChecker.isPitNearp) &&
               _groundChecker.IsGrounded();
    }

    protected override bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose && _isRunningFromThreat == false;
    }

    protected void StartRandomMovement()
    {
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

            StopStaying();
            
            _randomMovementCoroutine = null;
        }
    }

    protected IEnumerator RandomMovementRoutine()
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

            yield return new WaitForSeconds(_changeDirectionDelay +
                                            Random.Range(0, _changeDirectionDelay));
        }
    }

    protected bool IsThreatClose()
    {
        return _transform.ContainsTransform(_detectionRadius, _killableTargetDetection.ClosestTarget.Transform);
    }

    protected void StartRunningFromThreat()
    {
        if (_runFromThreatCoroutine == null)
        {
            _runFromThreatCoroutine = StartCoroutine(RunFromThreatRoutine());

            _isRunningFromThreat = true;
        }
    }

    protected IEnumerator RunFromThreatRoutine()
    {
        while (true)
        {
            TurnAwayFromThreat();
            
            yield return new WaitForSeconds(_defaultDelay);
        }
    }

    protected void TurnAwayFromThreat()
    {
        Transform target = _killableTargetDetection.ClosestTarget.Transform;
        
        SetMovementDirection(_transform.position.x < target.transform.position.x ? -1 : 1);
    }

    protected void StartStaying()
    {
        _canMove = false;
        
        StopCheckingEnvironment();
    }

    protected void StopStaying()
    {
        _canMove = true;
        
        StartCheckingEnvironment();
    }

    protected void StopRunningFromThreat()
    {
        if (_runFromThreatCoroutine != null)
        {
            StopCoroutine(_runFromThreatCoroutine);

            _runFromThreatCoroutine = null;

            _isRunningFromThreat = false;
        }
    }

    protected override void SetFaceDirection(int direction)
    {
        base.SetFaceDirection(direction);
        
        _sign.OnScaleChanged(direction);
    }

    protected void OnDrawGizmosSelected()
    {
        if (_transform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _detectionRadius);

        if (_killableTargetDetection.ClosestTarget == null) return;
        
        Transform threat = _killableTargetDetection.ClosestTarget.Transform;
        
        Gizmos.DrawLine(_transform.position, threat.position);
        Gizmos.DrawCube(threat.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
}
