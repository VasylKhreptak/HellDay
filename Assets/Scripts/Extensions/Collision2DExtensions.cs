using UnityEngine;

public static class Collision2DExtensions
{
    public static float GetImpulse(this Collision2D collision2D)
    {
        float impulse = 0f;

        for (int i = 0; i < collision2D.contacts.Length; i++)
        {
            impulse += collision2D.contacts[i].normalImpulse;
        }

        return impulse;
    }
}
