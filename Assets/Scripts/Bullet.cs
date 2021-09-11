using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolSpawnedObject
{
    [SerializeField] private float _bulletSpeed = 3;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _lifeTime = 2f;

    private void Awake()
    {
        Messenger<Pools, GameObject>.AddListener(GameEvent.POOL_OBJECT_SPAWNED, OnObjectSpawn);
    }

    private void OnDestroy()
    {
        Messenger<Pools, GameObject>.RemoveListener(GameEvent.POOL_OBJECT_SPAWNED, OnObjectSpawn);
    }

    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    public void OnObjectSpawn(Pools pool, GameObject bullet)
    {
        if (pool != Pools.DefaultBullet || gameObject != bullet) return;
        
        SetMovement();
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