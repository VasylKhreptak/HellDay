using System;

public class Animal : DamageableObject
{
    public static Action onDeath;

    protected override void DeathActions()
    {
        onDeath?.Invoke();

        base.DeathActions();
    }
}