using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GrayZombieAtack : CommonZombieAtack
{
    [Header("Preferences")] 
    [SerializeField] private float _maxJumpRadius;
    [SerializeField] private float _minJumpRadius;
    [SerializeField] private float _jumpSpeed;


    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;

    protected override IEnumerator ControlAtackRoutine()
    {
        while (true)
        {
            if (CanAtack() == true)
            {
                Atack();
            }

            if (CanJump() == true)
            {
                Jump();
            }

            yield return new WaitForSeconds(_atackDelay);
        }
    }

    private bool CanJump()
    {
        Transform targetTransform = _killableTargetDetection.ClosestTarget.Transform;

        if (targetTransform.gameObject.activeSelf == false)
        {
            return false;
        }
            
        return _transform.ContainsTransform(_maxJumpRadius, targetTransform) == true &&
               _transform.ContainsTransform(_minJumpRadius, targetTransform) == false;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,
            _jumpSpeed);
    }
    
#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_transform.position, _maxJumpRadius);
        Gizmos.DrawWireSphere(_transform.position, _minJumpRadius);
    }
#endif
}