using UnityEngine;

public static class Vector3Extensions
{
    public static bool ContainsPosition(this Vector3 source, float radius, Vector3 position)
    {
        return Vector3.Distance(source, position) < radius;
    }
}
