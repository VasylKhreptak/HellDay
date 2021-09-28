using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("Preferences")] 
    [SerializeField] private Pools[] _playerDeathParts;
    
    private ObjectPooler _objectPooler;

    protected override void Start()
    {
        SetMaxHealth(_maxHealth);
                                                                                                                            
        _objectPooler = ObjectPooler.Instance;
    }

    protected override void SetMaxHealth(float maxHealth)
    {
        _health = _maxHealth;
        
        Messenger<float>.Broadcast(GameEvent.SET_MAX_HEALTH_BAR, _maxHealth);
    }
    
     public override void TakeDamage(float damage)
    {
        _health -= damage;
        Messenger<float>.Broadcast(GameEvent.SET_HEALTH_BAR, _health);
        
        if (IsDead() == true)
        {
            Messenger.Broadcast(GameEvent.PLAYER_DIED);

            SpawnBodyParts();
            
            gameObject.SetActive(false);
        }
    }
    
    private void SpawnBodyParts()
    {
        foreach (Pools part in _playerDeathParts)
        {
            _objectPooler.GetFromPool(part, transform.position, Quaternion.identity);
        }
    }
}
