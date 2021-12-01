public class UI_SaveHumanButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        HumanDetection.onHumanNear += SetAnimationState;
        Player.onPlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        HumanDetection.onHumanNear -= SetAnimationState;
        Player.onPlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        SetAnimationState(false);
    }
}
