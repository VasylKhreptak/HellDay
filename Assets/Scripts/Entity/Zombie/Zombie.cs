using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private Pools[] _zombieDeathParts;
    
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
            SpawnBodyParts();
            
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