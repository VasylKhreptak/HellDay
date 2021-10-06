using System;

public class UI_DoorButtonAnimation : UI_SlideAnimation
{
    private void Start()
    {
        HideElementBehindScreen();
    }

    private void OnEnable()
    {
        Messenger<AnimationType>.AddListener(GameEvent.ANIMATE_OPEN_DOOR_BUTTON, AnimateButton);
    }

    private void OnDisable()
    {
        Messenger<AnimationType>.RemoveListener(GameEvent.ANIMATE_OPEN_DOOR_BUTTON, AnimateButton);
    }

    private void AnimateButton(AnimationType animationType)
    {
        Animate(animationType, _duration, 0);
    }
}
