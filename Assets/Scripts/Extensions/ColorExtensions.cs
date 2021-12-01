using UnityEngine;

public static class ColorExtensions
{
    public static void SetAlpha(this Color c, ref Color color, float alphaValue)
    {
        color.a = alphaValue;
    }
}
