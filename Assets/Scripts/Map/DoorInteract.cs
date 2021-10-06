using UnityEngine;
using System.Linq;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] protected Transform _transform;
    [SerializeField] protected Door[] _doors;

    protected Door FindClosestDoor(Door[] doors)
    {
        Transform[] targetTransforms = doors.Select(x => x.Transform).ToArray();
        
        Transform closestTransform = _transform.FindClosestTransform(targetTransforms);
        
        return doors.First(x => x.Transform == closestTransform);
    }
}
