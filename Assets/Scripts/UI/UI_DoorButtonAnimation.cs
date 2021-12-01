using System;

public class UI_DoorButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        PlayerDoorInteract.onAnimateOpenDoorBtn += SetAnimationState;
    }

    private void OnDisable()
    {
        PlayerDoorInteract.onAnimateOpenDoorBtn -= SetAnimationState;
    }
}
