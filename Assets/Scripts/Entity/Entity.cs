using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected float _maxHealth;
    protected float _health;

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
        _health -= damage;

        if (IsDead() == true)
        {
            Destroy(gameObject);
        }
    }

    protected bool IsDead()
    {
        return _health <= 0;
    }
}
