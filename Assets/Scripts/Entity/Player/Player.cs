using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity, IKillable
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
        
        if (IsDead())
        {
            Messenger.Broadcast(GameEvent.PLAYER_DIED);

            SpawnBodyParts();
            
            gameObject.SetActive(false);
        }
    }
    
    private void SpawnBodyParts()
    {
        for (int i = 0; i < _playerDeathParts.Length; i++)
        {
            _objectPooler.GetFromPool(_playerDeathParts[i], transform.position, Quaternion.identity);
        }
    }
}
