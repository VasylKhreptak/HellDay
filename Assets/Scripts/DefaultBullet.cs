using System.Collections;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 3;
    // [SerializeField] private float _destroyTime = 3f;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D.velocity = transform.right * _bulletSpeed;
        
        // Destroy(gameObject, _destroyTime);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Destroy(gameObject);
    // }
}