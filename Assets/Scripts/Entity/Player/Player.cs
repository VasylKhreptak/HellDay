using System;

public class Player : DamageableObject
{
    public static Action onPlayerDied;
    public static Action<float> onSetMaxHealthBar;
    public static Action<float> onSetHealthBar;


    
    protected override void SetMaxHealth(float maxHealth)
    {
        _health = data.MAXHealth;
        
        onSetMaxHealthBar?.Invoke(data.MAXHealth);
    }

    protected override void DeathActions()
     {
         onPlayerDied?.Invoke();
         
         base.DeathActions();
     }

    public override void SetHealth(float health)
    {
        base.SetHealth(health);
        
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
     {
         onSetHealthBar?.Invoke(_health);
     }
}
