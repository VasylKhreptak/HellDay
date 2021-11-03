using System;
using UnityEngine;

public class Player : Entity, IDestroyable
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

    protected override void DeathActions()
     {
         Messenger.Broadcast(GameEvent.PLAYER_DIED);

         SpawnBodyParts();
            
         gameObject.SetActive(false);
     }

     protected override void OnTakeDamage()
     {
         base.OnTakeDamage();
         
         Messenger<float>.Broadcast(GameEvent.SET_HEALTH_BAR, _health);
     }
    
    private void SpawnBodyParts()
    {
        for (int i = 0; i < _playerDeathParts.Length; i++)
        {
            _objectPooler.GetFromPool(_playerDeathParts[i], transform.position, Quaternion.identity);
        }
    }
}
