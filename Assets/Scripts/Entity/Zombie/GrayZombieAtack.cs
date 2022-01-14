using System.Collections;
using UnityEngine;

public class GrayZombieAtack : CommonZombieAtack
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Gray Zombie Data")]
    [SerializeField] private GrayZombieAtackData _grayZombieData;

    protected override IEnumerator ControlAtackRoutine()
    {
        while (true)
        {
            if (CanAtack()) Atack();

            if (CanJump()) Jump();

            yield return new WaitForSeconds(_grayZombieData.AtackDelay);
        }
    }

    private bool CanJump()
    {
        var target = _damageableTargetDetection._closestTarget.Transform;

        if (target == null ||
            target.gameObject.activeSelf == false) return false;

        return _transform.position.ContainsPosition(_grayZombieData.MAXJumpRadius, target.position) &&
               _transform.position.ContainsPosition(_grayZombieData.MINJumpRadius, target.position) == false;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _grayZombieData.JumpSpeed);
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireSphere(_transform.position, _grayZombieData.MAXJumpRadius);
        Gizmos.DrawWireSphere(_transform.position, _grayZombieData.MINJumpRadius);
    }
}