public class UI_SaveHumanButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        Messenger<bool>.AddListener(GameEvents.ANIMATE_SAVE_HUMAN_BUTTON, SetAnimationState);
        Messenger.AddListener(GameEvents.PLAYER_DIED, OnPlayerDeath);
    }

    private void OnDisable()
    {
        Messenger<bool>.RemoveListener(GameEvents.ANIMATE_SAVE_HUMAN_BUTTON, SetAnimationState);
        Messenger.RemoveListener(GameEvents.PLAYER_DIED, OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        SetAnimationState(false);
    }
}
