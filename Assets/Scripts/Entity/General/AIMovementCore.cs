using UnityEngine;
using System.Collections;

public class AIMovementCore : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    
    [Header("MovementPreferences")] 
    [SerializeField] protected float _movementSpeed = 13f;
    [SerializeField] protected float _jumpSpeed = 18f;
    
    [Header("Delays")]
    [SerializeField] protected float _changeDirectionDelay = 3f;
    [SerializeField] protected float _defaultDelay = 0.5f;
    [SerializeField] protected float _obstacleCheckDelay = 0.3f;
    
    [Header("Environment checkers")]
    [SerializeField] protected GroundChecker _groundChecker;
    [SerializeField] protected ObstacleChecker _obstacleChecker;
    [SerializeField] protected BarrierChecker _barrierChecker;
    
    protected Coroutine _randomMovementCoroutine;
    protected Coroutine _checkEnvironmentCorouitne;
    
    protected virtual void Update()
    { 
        _rigidbody2D.velocity = new Vector2(_movementSpeed, _rigidbody2D.velocity.y);
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
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpSpeed);
    }
    
    protected virtual bool CanReverseMovementDirection()
    {
        return _barrierChecker.isBarrierClose;
    }
    
    protected void ReverseMovementDirection()
    {
        _movementSpeed *= -1;
        
        SetFaceDirection((int) Mathf.Sign(_movementSpeed));
    }
    
    protected virtual void SetFaceDirection(int direction)
    {
        _transform.localScale = new Vector3(direction, 1, 1);
    }
    
    protected void SetMovementDirection(int direction)
    {
        _movementSpeed = Mathf.Sign(direction) * Mathf.Abs(_movementSpeed);
        
        SetFaceDirection(direction);
    }
}
