using System;

public class UI_DoorButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        Messenger<bool>.AddListener(GameEvents.ANIMATE_OPEN_DOOR_BUTTON, SetAnimationState);
    }

    private void OnDisable()
    {
        Messenger<bool>.RemoveListener(GameEvents.ANIMATE_OPEN_DOOR_BUTTON, SetAnimationState);
    }
}
