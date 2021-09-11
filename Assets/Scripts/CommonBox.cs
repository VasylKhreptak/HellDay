using System;
using UnityEngine;

public class CommonBox : Box
{
    [SerializeField] private int _durability;

    protected override int Durability
    {
        get { return _durability; }
        set { _durability = value; }
    }

    protected override bool IsBroken()
    {
        return Durability <= 0;
    }

    protected override void TakeDamage(int damage)
    {
        Durability -= damage;

        if (IsBroken())
        {
            SpawnDestroyParticle();

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
            TakeDamage(1);
    }

    protected override void SpawnDestroyParticle()
    {
        GameObject particle = _objectPooler.GetFromPool(Pools.BoxDestroyParticle);
        particle.transform.position = transform.position;

        Messenger<Pools, GameObject>.Broadcast(GameEvent.POOL_OBJECT_SPAWNED, Pools.BoxDestroyParticle, particle);
    }
}