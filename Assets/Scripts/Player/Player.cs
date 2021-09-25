using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private float _maxHealth = 100f;
    
    private float _health;

    private void Start()
    {
        SetMaxHealth(_maxHealth);
    }

    private void SetMaxHealth(float maxHealth)
    {
        _health = _maxHealth;
        Messenger<float>.Broadcast(GameEvent.SET_MAX_HEALTH_BAR, _maxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        Messenger<float>.Broadcast(GameEvent.SET_HEALTH_BAR, _health);
        
        if (IsDead() == true)
        {
            DeathACtions();
        }
    }

    private bool IsDead()
    {
        return _health <= 0;
    }

    private void DeathACtions()
    {
        
    }
}
