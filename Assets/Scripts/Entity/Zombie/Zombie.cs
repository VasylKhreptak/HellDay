using System;

public class Zombie : DamageableObject
{
    public static Action onDeath;

    protected override void DeathActions()
    {
        onDeath?.Invoke();

        base.DeathActions();
    }
}