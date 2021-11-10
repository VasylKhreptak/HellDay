public class Player : Entity, IKillable
{
    protected override void SetMaxHealth(float maxHealth)
    {
        _health = _maxHealth;
        
        Messenger<float>.Broadcast(GameEvents.SET_MAX_HEALTH_BAR, _maxHealth);
    }

    protected override void DeathActions()
     {
         Messenger.Broadcast(GameEvents.PLAYER_DIED);
         
         gameObject.SetActive(false);
     }

     public void UpdateHealthBar()
     {
         Messenger<float>.Broadcast(GameEvents.SET_HEALTH_BAR, _health);
     }
}
