using System;

public class Zombie : DamageableObject
{
    public static Action onDeath;

    protected override void DeathActions()
    {
        base.DeathActions();

        onDeath?.Invoke();
    }
}