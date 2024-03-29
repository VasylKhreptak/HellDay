using UnityEngine;

namespace Extensions
{
    public static class Quaternion
    {
        public static UnityEngine.Quaternion RotationFromDirection2D(UnityEngine.Vector2 direction)
        {
            var angle = UnityEngine.Mathf.Atan2(direction.y, direction.x) * UnityEngine.Mathf.Rad2Deg;

            return UnityEngine.Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}