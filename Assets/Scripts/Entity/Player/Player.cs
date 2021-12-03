using System;

public class Player : DamageableObject
{
    public static Action onPlayerDied;
    public static Action<float> onSetMaxHealthBar;
    public static Action<float> onSetHealthBar;
    public static Action onResurrection;

    private bool _wasDied;

    private void OnEnable()
    {
        if (_wasDied)
        {
            onResurrection?.Invoke();
        }
    }

    protected override void SetMaxHealth(float maxHealth)
    {
        _health = data.MAXHealth;
        
        onSetMaxHealthBar?.Invoke(data.MAXHealth);
    }

    protected override void DeathActions()
     {
         onPlayerDied?.Invoke();
         
         base.DeathActions();

         _wasDied = true;
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
