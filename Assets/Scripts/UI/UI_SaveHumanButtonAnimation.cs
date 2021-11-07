public class UI_SaveHumanButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        Messenger<bool>.AddListener(GameEvent.ANIMATE_SAVE_HUMAN_BUTTON, SetAnimationState);
        Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDeath);
    }

    private void OnDisable()
    {
        Messenger<bool>.RemoveListener(GameEvent.ANIMATE_SAVE_HUMAN_BUTTON, SetAnimationState);
        Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        SetAnimationState(false);
    }
}
