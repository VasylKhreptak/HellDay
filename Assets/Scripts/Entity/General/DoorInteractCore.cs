using System;
using UnityEngine;

public class DoorInteractCore : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _doorLayerMask;

    protected Door _closestDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_doorLayerMask.ContainsLayer(other.gameObject.layer))
            if (other.TryGetComponent(out Door door))
            {
                _closestDoor = door;

                OnEnteredDoorArea();
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_doorLayerMask.ContainsLayer(other.gameObject.layer))
        {
            OnExitDoorArea();

            _closestDoor = null;
        }
    }

    protected virtual void OnEnteredDoorArea()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnExitDoorArea()
    {
        throw new NotImplementedException();
    }
}