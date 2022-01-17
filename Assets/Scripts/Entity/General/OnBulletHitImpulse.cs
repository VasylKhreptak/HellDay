using System;
using UnityEngine;

public class OnBulletHitImpulse : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private OnBulletHitEvent _onBulletHitEvent;
    
    [Header("Data")]
    [SerializeField] private OnBulletHitImpulseData _data;

    private void OnEnable()
    {
        _onBulletHitEvent.onHit += ProcessCollision;
    }

    private void OnDisable()
    {
        _onBulletHitEvent.onHit -= ProcessCollision;
    }

    private void ProcessCollision(Collision2D collision2D)
    {
        Vector2 dir = collision2D.gameObject.transform.right;
        
        _rigidbody2D.AddForceAtPosition(dir * _data.Impulse, 
            collision2D.GetContact(0).point, ForceMode2D.Impulse);
    }
}
