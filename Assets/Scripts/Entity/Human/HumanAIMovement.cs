using System.Collections;
using UnityEngine;

public class HumanAIMovement : AIMovementCore
{
    [Header("Movement Data")]
    [SerializeField] private HumanAIMovementData _movementData;

    [Header("Environment checkers")]
    [SerializeField] protected PitChecker _pitChecker;

    [Header("Threat detection")]
    [SerializeField] protected CommonTargetDetection _commonTargetDetectin;

    [Header("Sign Scale Compensate")]
    [SerializeField] private SignScaleCompensate _sign;

    protected bool _canMove = true;
    protected bool _isRunningFromThreat;

    protected Coroutine _runFromThreatCoroutine;

    protected override void FixedUpdate()
    {
        if (_canMove) base.FixedUpdate();
    }

    protected void Start()
    {
        StartCheckingEnvironment();

        StartCoroutine(ControlMovementRoutine());
    }

    protected void StartCheckingEnvironment()
    {
        if (_checkEnvironmentCorouitne == null) _checkEnvironmentCorouitne = StartCoroutine(CheckEnvironmentRoutine());
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

            yield return new WaitForSeconds(_dataCore.DefaultDelay);
        }
    }

    protected override bool CanJump()
    {
        return (_obstacleChecker.isObstacleClose || _pitChecker.isPitNearp) &&
               _groundChecker.IsGrounded();
    }

    protected override bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose && _isRunningFromThreat == false;
    }

    protected void StartRandomMovement()
    {
        if (_randomMovementCoroutine == null) _randomMovementCoroutine = StartCoroutine(RandomMovementRoutine());
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

            yield return new WaitForSeconds(_dataCore.ChangeDirectionDelay +
                                            Random.Range(0, _dataCore.ChangeDirectionDelay));
        }
    }

    protected bool IsThreatClose()
    {
        if (_commonTargetDetectin.ClosestTarget == null) return false;

        return _transform.position.ContainsPosition(_movementData.DetectionRadius,
            _commonTargetDetectin.ClosestTarget.position);
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

            yield return new WaitForSeconds(_dataCore.DefaultDelay);
        }
    }

    protected void TurnAwayFromThreat()
    {
        var target = _commonTargetDetectin.ClosestTarget;

        if (target == null) return;

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
        Gizmos.DrawWireSphere(_transform.position, _movementData.DetectionRadius);

        if (_commonTargetDetectin.ClosestTarget == null) return;

        var threat = _commonTargetDetectin.ClosestTarget;

        Gizmos.DrawLine(_transform.position, threat.position);
        Gizmos.DrawCube(threat.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
}