using System;

public class PlayerDoorInteract : DoorInteractCore
{
    public static Action<bool> onAnimateOpenDoorBtn;
    
    protected override void OnEnteredDoorArea()
    {
        onAnimateOpenDoorBtn?.Invoke(true);
    }

    protected override void OnExitDoorArea()
    {
        onAnimateOpenDoorBtn?.Invoke(false);
    }

    public void ToggleDoor()
    {
        if (_closestDoor)
        {
            _closestDoor.ToggleDoor();
        }
    }
}
