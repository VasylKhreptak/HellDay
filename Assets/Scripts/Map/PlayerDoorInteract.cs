public class PlayerDoorInteract : DoorInteract
{
    public void ToggleDoor()
    {
        Door door = FindClosestDoor(_doors);
        
        door.ToggleDoor();
    }
}
