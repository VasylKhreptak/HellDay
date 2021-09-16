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
        if (other.collider.CompareTag("Bullet") == true)
            TakeDamage(1);
    }

    protected override void SpawnDestroyParticle()
    {
        _objectPooler.GetFromPool(Pools.BoxDestroyParticle, transform.position, Quaternion.identity);
    }
}