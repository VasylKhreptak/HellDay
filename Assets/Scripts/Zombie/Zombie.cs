using Unity.Mathematics;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;

        if (IsDead() == true)
        {
            SpawnZombieParticles();
            
            Destroy(gameObject);
        }
    }

    private bool IsDead()
    {
        return _health <= 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            TakeDamage(WeaponManager.defaultBulletDamage);
        }
    }

    private void SpawnZombieParticles()
    {
        Pools[] pools = 
        {
            Pools.ZombieDeathParticle,
            Pools.ZombieHead,
            Pools.ZombieBody,
            Pools.ZombieLeg,
            Pools.ZombieLeg,
            Pools.ZombieArm,
            Pools.ZombieArm
        };

        foreach (var pool in pools)
        {
            _objectPooler.GetFromPool(pool, transform.position,
                quaternion.identity);
        }
    }
}