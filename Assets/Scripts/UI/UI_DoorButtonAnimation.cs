public class UI_DoorButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        PlayerDoorInteract.onEnterDoorArea += Show;
        PlayerDoorInteract.onExitDoorArea += Hide;
    }

    private void OnDisable()
    {
        PlayerDoorInteract.onEnterDoorArea -= Show;
        PlayerDoorInteract.onExitDoorArea -= Hide;
    }
}