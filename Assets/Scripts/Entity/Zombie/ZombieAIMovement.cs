using System.Collections;
using UnityEngine;

public class ZombieAIMovement : AIMovementCore
{
    [Header("References")]
    [SerializeField] protected DamageableTargetDetection _damageableTargetDetection;

    [Header("Target Detection Preferences")]
    [SerializeField] private float _mainDetectionRadius;

    [Header("Movement Data")]
    [SerializeField] private ZombieAIMovementData _movementData;

    protected Coroutine _followTargetCoroutine;
    protected Coroutine _increaseDetectionRadiusCoroutine;

    protected bool _isFollowingTarget;
    protected float previousDetectionRadius;


    protected void OnEnable()
    {
        ExplosiveObjectCore.onPlayedLoudSound += OnPlayedLoudSound;
        WeaponCore.onShoot += OnPlayedLoudSound;
    }

    protected void OnDisable()
    {
        ExplosiveObjectCore.onPlayedLoudSound -= OnPlayedLoudSound;
        WeaponCore.onShoot -= OnPlayedLoudSound;
    }

    protected virtual void Start()
    {
        previousDetectionRadius = _mainDetectionRadius;

        StartCoroutine(CheckEnvironmentRoutine());

        StartCoroutine(ControlMovementRoutine());
    }

    protected void OnPlayedLoudSound()
    {
        var target = _damageableTargetDetection._closestTarget.Transform;

        if (target == null || _transform == null) return;

        StartCoroutine(ControlDetectionRadius());
    }

    protected IEnumerator ControlDetectionRadius()
    {
        _mainDetectionRadius = _movementData.AudioDetectionRadius;

        yield return new WaitForSeconds(_movementData.IncDetectionRadiusDur);

        _mainDetectionRadius = previousDetectionRadius;
    }

    protected void StartRandomMovement()
    {

        if (_randomMovementCoroutine == null)
        {
            _isFollowingTarget = false;

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
            if (Random.Range(0, 2) == 1) ReverseMovementDirection();

            yield return new WaitForSeconds(_dataCore.ChangeDirectionDelay +
                                            Random.Range(0, _dataCore.ChangeDirectionDelay));
        }
    }

    protected override bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose && _isFollowingTarget == false;
    }

    protected IEnumerator ControlMovementRoutine()
    {
        while (true)
        {
            if (CanFollowTarget())
            {
                StopRandomMovement();
                StartFollowingClosestTarget();
            }
            else
            {
                StopFollowingClosestTarget();
                StartRandomMovement();
            }

            yield return new WaitForSeconds(_dataCore.DefaultDelay);
        }
    }

    protected bool CanFollowTarget()
    {
        var closestTarget = _damageableTargetDetection._closestTarget.Transform;

        if (closestTarget == null) return false;

        return _transform.position.ContainsPosition(_mainDetectionRadius, closestTarget.position);
    }

    protected void StartFollowingClosestTarget()
    {
        if (_followTargetCoroutine == null)
        {
            _followTargetCoroutine = StartCoroutine(FollowClosestTargetRoutine());

            _isFollowingTarget = true;
        }
    }

    protected void StopFollowingClosestTarget()
    {
        if (_followTargetCoroutine != null)
        {
            StopCoroutine(_followTargetCoroutine);

            _followTargetCoroutine = null;

            _isFollowingTarget = false;
        }
    }

    protected IEnumerator FollowClosestTargetRoutine()
    {
        while (true)
        {
            var closestTarget = _damageableTargetDetection._closestTarget.Transform;

            LookAtTaregt(closestTarget);

            yield return new WaitForSeconds(_dataCore.DefaultDelay);
        }
    }

    protected void LookAtTaregt(Transform target)
    {
        if (target == null) return;

        SetMovementDirection(_transform.position.x < target.transform.position.x ? 1 : -1);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_transform.position, _mainDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _movementData.AudioDetectionRadius);
    }
}
