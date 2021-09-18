using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _bulletSpeed = 3;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _lifeTime = 2f;

    private ObjectPooler _objectPooler;

    public void OnEnable()
    {
        SetMovement();
    }

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity = transform.right * _bulletSpeed;

        if (gameObject.activeSelf == true)
            StartCoroutine(DisableObject(_lifeTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint2D = other.GetContact(0);
        Vector2 hitPosition = contactPoint2D.point;

        if (other.collider.CompareTag("Zombie") == true)
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