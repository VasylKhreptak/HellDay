using System;
using UnityEngine;
using System.Collections;

public class AIMovementCore : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    
    [Header("MovementPreferences")] 
    [SerializeField] protected float _movementForce = 13f;
    [SerializeField] protected float _jumpForce = 18f;
    [SerializeField] protected ForceMode2D _forceMode2D;
    [SerializeField] protected float _maxHorVelocity = 5f;
    
    [Header("Delays")]
    [SerializeField] protected float _changeDirectionDelay = 3f;
    [SerializeField] protected float _defaultDelay = 0.5f;
    [SerializeField] protected float _obstacleCheckDelay = 0.3f;
    
    [Header("Environment checkers")]
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected ObstacleChecker _obstacleChecker;
    [SerializeField] protected BarrierChecker _barrierChecker;

    [Header("Configurable Update")] 
    [SerializeField] protected int _frameRate = 5;
    
    protected Coroutine _randomMovementCoroutine;
    protected Coroutine _checkEnvironmentCorouitne;

    protected virtual void FixedUpdate()
    {
        _rigidbody2D.AddForce(new Vector2(_movementForce, 0), _forceMode2D);
        _rigidbody2D.LimitHorizontalVelocity(_maxHorVelocity);
    }

    protected IEnumerator CheckEnvironmentRoutine()
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
    
    protected virtual bool CanJump()
    {
        return _obstacleChecker.isObstacleClose &&
               _groundChecker.IsGrounded();
    }
    
    protected void Jump()
    {
        _rigidbody2D.AddForce(new Vector2(0, _jumpForce), _forceMode2D);
    }
    
    protected virtual bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose;
    }
    
    protected void ReverseMovementDirection()
    {
        _movementForce *= -1;
        
        SetFaceDirection((int) Mathf.Sign(_movementForce));
    }
    
    protected virtual void SetFaceDirection(int direction)
    {
        _transform.localScale = new Vector3(direction, 1, 1);
    }
    
    protected void SetMovementDirection(int direction)
    {
        _movementForce = Mathf.Sign(direction) * Mathf.Abs(_movementForce);
        
        SetFaceDirection(direction);
    }
}
