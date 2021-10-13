using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Door[] _doors;

    protected Door FindClosestDoor(Door[] doors)
    {
        Transform[] targetTransforms = SelectTransforms(doors);
        
        Transform closestTransform = _transform.FindClosestTransform(targetTransforms);

        return FindFirst(doors, closestTransform);
    }
    
    private Transform[] SelectTransforms(Door[] doors)
    {
        Transform[] transforms = new Transform[doors.Length];
        
        for (int i = 0; i < doors.Length; i++)
        {
            transforms[i] = doors[i].Transform;
        }

        return transforms;
    }

    private Door FindFirst(Door[] doors, Transform transform)
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].Transform == transform)
            {
                return doors[i];
            }
        }

        return null;
    }
}
