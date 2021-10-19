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

        if (IsDead())
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
        for (var i = 0; i < _zombieDeathParts.Length; i++)
        {
            var part = _zombieDeathParts[i];
            _objectPooler.GetFromPool(part, _transform.position,
                Quaternion.identity);
        }
    }
}