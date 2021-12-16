using UnityEngine;

public static class ColorExtensions
{
    public static Color WithAlpha(this Color color, float alphaValue)
    {
        return new Color(color.r, color.g, color.b, alphaValue);
    }
}
