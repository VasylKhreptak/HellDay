using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [Header("Data")] 
    public DamageableObjectData data;
    
    protected float _health;

    [Header("Damage Event")] 
    [SerializeField] private UnityEvent<float> OnTakeDamage;

    public bool IsDead => _health <= 0;
    public float Health => _health; 

    protected virtual void Start()
    {
        SetMaxHealth(data.MAXHealth);
    }

    protected virtual void SetMaxHealth(float maxHealth)
    {
        _health = maxHealth;
    }

    public virtual void SetHealth(float health)
    {
        _health = Mathf.Clamp(health, 0, data.MAXHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        SetHealth(_health - damage);

        OnTakeDamage?.Invoke(damage);
        
        if (IsDead)
        {
           DeathActions();
        }
    }

    protected virtual void DeathActions()
    {
        if (data.CanBeDestroyed)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
