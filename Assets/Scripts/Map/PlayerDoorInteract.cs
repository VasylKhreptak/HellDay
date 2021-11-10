public class PlayerDoorInteract : DoorInteractCore
{
    protected override void OnEnteredDoorArea()
    {
        Messenger<bool>.Broadcast(GameEvents.ANIMATE_OPEN_DOOR_BUTTON,true);
    }

    protected override void OnExitDoorArea()
    {
        Messenger<bool>.Broadcast(GameEvents.ANIMATE_OPEN_DOOR_BUTTON,false);
    }

    public void ToggleDoor()
    {
        if (_closestDoor)
        {
            _closestDoor.ToggleDoor();
        }
    }
}
