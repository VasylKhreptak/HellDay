using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ZombieAIMovement : AIMovementCore
{
    [Header("References")]
    [SerializeField] protected KillableTargetDetection _killableTargetDetection;
    
    [Header("Target detection preferences")] 
    [SerializeField] protected float _mainDetectionRadius = 5f;
    [SerializeField] protected float _audioDetectionRadius;
    [SerializeField] protected float _increaseDetectionRadiusTime = 10f;
    
    protected Coroutine _followTargetCoroutine;
    protected Coroutine _increaseDetectionRadiusCoroutine;

    protected bool _isFollowingTarget;


    protected void OnEnable()
    {
        Messenger.AddListener(GameEvents.PLAYED_LOUD_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    protected void OnDisable()
    {
        Messenger.RemoveListener(GameEvents.PLAYED_LOUD_AUDIO_SOURCE, OnPlayedAudioSource);
    }

    protected virtual void Start()
    {
        StartCoroutine(CheckEnvironmentRoutine());

        StartCoroutine(ControlMovementRoutine());
    }
    
    protected void OnPlayedAudioSource()
    {
        if (Vector2.Distance(_transform.position,
            _killableTargetDetection.ClosestTarget.Transform.position) < _audioDetectionRadius)
        {
            float previousDetectionRadius = _mainDetectionRadius;
            _mainDetectionRadius = _audioDetectionRadius;

            this.DOWait(_increaseDetectionRadiusTime).OnComplete(() =>
            {
                _mainDetectionRadius = previousDetectionRadius;
            });
        }
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

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_transform.position, _mainDetectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, _audioDetectionRadius);
    }
}