using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] protected int _durability = 7;
    protected ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected bool IsBroken()
    {
        return _durability <= 0;
    }

    protected void TakeDamage(int damage)
    {
        _durability -= damage;

        if (IsBroken())
        {
            SpawnDestroyParticle();
            
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet") == true)
            TakeDamage(1);
    }

    protected void SpawnDestroyParticle()
    {
        _objectPooler.GetFromPool(Pools.BoxDestroyParticle, transform.position, Quaternion.identity);
    }
}