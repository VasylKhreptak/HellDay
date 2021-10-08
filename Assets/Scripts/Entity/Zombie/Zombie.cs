using UnityEngine;

public class Zombie : Entity, IKillable
{
    [Header("Preferences")] 
    [SerializeField] private Transform _transform;
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
            TakeDamage(WeaponControl.defaultBulletDamage);
        }
    }

    public void SpawnBodyParts()
    {
        foreach (var part in _zombieDeathParts)
        {
            _objectPooler.GetFromPool(part, _transform.position,
                Quaternion.identity);
        }
    }
}