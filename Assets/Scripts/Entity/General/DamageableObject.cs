using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [Header("Preferences")] 
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected bool _canBeDestroyed = true;
    
    protected float _health;

    [Header("Damage Event")] 
    [SerializeField] private UnityEvent OnTakeDamage;

    protected bool isDead => _health <= 0;
    public float health => _health;
    public float maxHealth => _maxHealth;
    
    protected virtual void Start()
    {
        SetMaxHealth(_maxHealth);
    }

    protected virtual void SetMaxHealth(float maxHealth)
    {
        _health = maxHealth;
    }

    public virtual void SetHealth(float health)
    {
        _health = Mathf.Clamp(health, 0, _maxHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        SetHealth(_health - damage);

        OnTakeDamage.Invoke();
        
        if (isDead)
        {
           DeathActions();
        }
    }

    protected virtual void DeathActions()
    {
        if (_canBeDestroyed)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
