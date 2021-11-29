using System;
using UnityEngine;
using System.Collections;

public class AIMovementCore : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Rigidbody2D _rigidbody2D;

    [Header("Data core")] 
    [SerializeField] protected AIMovementCoreData _dataCore;

    [Header("Movement Preferences")] 
    [SerializeField]
    private float _movementForce = 6f;
    
    [Header("Environment checkers")]
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected ObstacleChecker _obstacleChecker;
    [SerializeField] protected BarrierChecker _barrierChecker;

    protected Coroutine _randomMovementCoroutine;
    protected Coroutine _checkEnvironmentCorouitne;

    protected virtual void FixedUpdate()
    {
        _rigidbody2D.AddForce(new Vector2(_movementForce, 0), _dataCore.ForceMode2D);
        _rigidbody2D.LimitHorizontalVelocity(_dataCore.MAXHorVelocity);
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

            yield return new WaitForSeconds(_dataCore.ObstacleCheckDelay);
        }
    }
    
    protected virtual bool CanJump()
    {
        return _obstacleChecker.isObstacleClose &&
               _groundChecker.IsGrounded();
    }
    
    protected void Jump()
    {
        _rigidbody2D.AddForce(new Vector2(0, _dataCore.JumpForce), _dataCore.ForceMode2D);
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
