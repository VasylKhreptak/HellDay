using UnityEngine;

public class Zombie : Entity
{
    [Header("Preferences")]
    [SerializeField] private Pools[] _zombieDeathParts;

    private ObjectPooler _objectPooler;

    protected override void Start()
    {
        SetMaxHealth(_maxHealth);
        
        _objectPooler = ObjectPooler.Instance;
    }

    public override void TakeDamage(float damage)
    {
        _health -= damage;

        if (IsDead() == true)
        {
            SpawnBodyParts();
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            TakeDamage(WeaponController.defaultBulletDamage);
        }
    }

    private void SpawnBodyParts()
    {
        foreach (var part in _zombieDeathParts)
        {
            _objectPooler.GetFromPool(part, transform.position,
                Quaternion.identity);
        }
    }
}