public class UI_SaveHumanButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        Messenger<bool>.AddListener(GameEvent.ANIMATE_SAVE_HUMAN_BUTTON, SetAnimationState);
    }

    private void OnDisable()
    {
        Messenger<bool>.AddListener(GameEvent.ANIMATE_SAVE_HUMAN_BUTTON, SetAnimationState);
    }
}
