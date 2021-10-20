using System;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _transform;
    
    [Header("Preferences")]
    [SerializeField] private float _bulletSpeed = 3;
    [SerializeField] private float _lifeTime = 2f;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void OnEnable()
    {   
        SetMovement();
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity = transform.right * _bulletSpeed;

        if (gameObject.activeSelf)
        {
            _transform.DOWait(_lifeTime, () => { gameObject.SetActive(false); });
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint2D = other.GetContact(0);
        Vector2 hitPosition = contactPoint2D.point;

        if (other.collider.CompareTag("Zombie"))
        {
            _objectPooler.GetFromPool(Pools.ZombieHitParticle,
                hitPosition, Quaternion.identity);
        }
        else
        {
            _objectPooler.GetFromPool(Pools.HitParticle,
                hitPosition, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}