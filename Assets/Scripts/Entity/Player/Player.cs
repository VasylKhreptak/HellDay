public class Player : Entity, IKillable
{
    protected override void SetMaxHealth(float maxHealth)
    {
        _health = _maxHealth;
        
        Messenger<float>.Broadcast(GameEvent.SET_MAX_HEALTH_BAR, _maxHealth);
    }

    protected override void DeathActions()
     {
         Messenger.Broadcast(GameEvent.PLAYER_DIED);
         
         gameObject.SetActive(false);
     }

     protected override void OnTakeDamage()
     {
         _onDamageReact.ReactOnHit();
         
         Messenger<float>.Broadcast(GameEvent.SET_HEALTH_BAR, _health);
     }
}
