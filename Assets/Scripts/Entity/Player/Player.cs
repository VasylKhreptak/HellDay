using System;

public class Player : DamageableObject
{
    public static Action onDie;
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
        _health = MAXHealth;

        onSetMaxHealthBar?.Invoke(MAXHealth);
    }

    protected override void DeathActions()
    {
        onDie?.Invoke();

        base.DeathActions();

        _wasDied = true;
    }

    public override void SetHealth(float health)
    {
        base.SetHealth(health);

        onSetHealthBar?.Invoke(_health);
    }
}