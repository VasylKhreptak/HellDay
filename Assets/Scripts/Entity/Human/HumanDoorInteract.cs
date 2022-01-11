public class HumanDoorInteract : DoorInteractCore
{
    protected override void OnEnteredDoorArea()
    {
        if (_closestDoor) _closestDoor.SetDoorState(true);
    }

    protected override void OnExitDoorArea()
    {
        if (_closestDoor) _closestDoor.SetDoorState(false);
    }
}