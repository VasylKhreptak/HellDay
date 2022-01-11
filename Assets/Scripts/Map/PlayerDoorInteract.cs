using System;

public class PlayerDoorInteract : DoorInteractCore
{
    public static Action onEnterDoorArea;
    public static Action onExitDoorArea;

    protected override void OnEnteredDoorArea()
    {
        onEnterDoorArea?.Invoke();
    }

    protected override void OnExitDoorArea()
    {
        onExitDoorArea?.Invoke();
    }

    public void ToggleDoor()
    {
        if (_closestDoor) _closestDoor.ToggleDoor();
    }
}