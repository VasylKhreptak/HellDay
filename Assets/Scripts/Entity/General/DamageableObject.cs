using System;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [Header("Data")] 
    [SerializeField] private DamageableObjectData _data;
    
    protected float _health;

    public Action<float> onTakeDamage; 
    
    public bool IsDead => _health <= 0;
    public float Health => _health;
    public float MAXHealth => _data.MAXHealth;

    protected virtual void Start()
    {
        SetMaxHealth(_data.MAXHealth);
    }

    protected virtual void SetMaxHealth(float maxHealth)
    {
        _health = maxHealth;
    }

    public virtual void SetHealth(float health)
    {
        _health = Mathf.Clamp(health, 0, _data.MAXHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        SetHealth(_health - damage);

        onTakeDamage?.Invoke(damage);
        
        if (IsDead)
        {
           DeathActions();
        }
    }

    protected virtual void DeathActions()
    {
        if (_data.CanBeDestroyed)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
