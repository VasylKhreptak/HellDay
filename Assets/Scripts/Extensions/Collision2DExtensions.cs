using UnityEngine;

public static class Collision2DExtensions
{
    public static float GetImpulse(this Collision2D collision2D)
    {
        var impulse = 0f;

        foreach (var contactPoint in collision2D.contacts) impulse += contactPoint.normalImpulse;

        return impulse;
    }
}