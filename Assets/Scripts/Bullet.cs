using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _bulletSpeed = 3;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _lifeTime = 2f;


    public void OnEnable()
    {
        SetMovement();
    }

    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity = transform.right * _bulletSpeed;

        if (gameObject.activeSelf)
            StartCoroutine(DisableObject(_lifeTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Messenger<Collision2D>.Broadcast(GameEvent.BULLET_HIT, other);
        
        gameObject.SetActive(false);
    }
}