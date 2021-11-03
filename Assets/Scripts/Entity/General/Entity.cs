using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected float _maxHealth;
    protected float _health;

    [Header("Hit React")] 
    [SerializeField] protected OnDamageReact _onDamageReact;

    protected virtual void Start()
    {
        SetMaxHealth(_maxHealth);
    }

    protected virtual void SetMaxHealth(float maxHealth)
    {
        _health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        OnTakeDamage();

        _health -= damage;

        if (IsDead)
        {
           DeathActions();
        }
    }

    protected virtual void DeathActions()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTakeDamage()
    {
        _onDamageReact.ReactOnHit();
    }

    protected bool IsDead => _health <= 0;
}
