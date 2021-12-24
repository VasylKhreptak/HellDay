using UnityEngine;

namespace Extensions
{
    public static class Quaternion
    {
        public static UnityEngine.Quaternion RotationFromDirection2D(Vector2 direction)
        {
            float angle = UnityEngine.Mathf.Atan2(direction.y, direction.x) * UnityEngine.Mathf.Rad2Deg;
        
            return UnityEngine.Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
