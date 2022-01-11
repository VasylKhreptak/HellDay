using System;

public class PhysicalObject : DamageableObject
{
    public static Action onDestroy;

    protected override void DeathActions()
    {
        onDestroy?.Invoke();

        base.DeathActions();
    }
}