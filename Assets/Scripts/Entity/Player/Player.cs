public class Player : DamageableObject
{
    protected override void SetMaxHealth(float maxHealth)
    {
        _health = data.MAXHealth;
        
        Messenger<float>.Broadcast(GameEvents.SET_MAX_HEALTH_BAR, data.MAXHealth);
    }

    protected override void DeathActions()
     {
         Messenger.Broadcast(GameEvents.PLAYER_DIED);
         
         base.DeathActions();
     }

    public override void SetHealth(float health)
    {
        base.SetHealth(health);
        
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
     {
         Messenger<float>.Broadcast(GameEvents.SET_HEALTH_BAR, _health);
     }
}
