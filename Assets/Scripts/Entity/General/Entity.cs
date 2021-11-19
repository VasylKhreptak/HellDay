using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected float _maxHealth;
    protected float _health;

    [Header("Hit React")] 
    [SerializeField] protected OnDamageColor _onDamageColor;

    [Header("Damage Event")] 
    [SerializeField] private UnityEvent OnTakeDamage;

    protected bool IsDead => _health <= 0;
    public float Health => _health;
    
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
        
        if (IsDead)
        {
           DeathActions();
        }
    }

    protected virtual void DeathActions()
    {
        Destroy(gameObject);
    }
}
