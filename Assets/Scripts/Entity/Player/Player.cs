using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Pools[] _playerDeathParts;
    
    private float _health;
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;

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
            Messenger.Broadcast(GameEvent.PLAYER_DIED);

            SpawnBodyParts();
            
            gameObject.SetActive(false);
        }
    }

    private bool IsDead()
    {
        return _health <= 0;
    }

    private void SpawnBodyParts()
    {
        foreach (Pools part in _playerDeathParts)
        {
            _objectPooler.GetFromPool(part, transform.position, Quaternion.identity);
        }
    }
}
