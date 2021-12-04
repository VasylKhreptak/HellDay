public class UI_SaveHumanButtonAnimation : UI_SlideAnimation
{
    private void OnEnable()
    {
        HumanDetection.onHumanNear += SetAnimationState;
        Player.onDie += OnPlayerDied;
    }

    private void OnDisable()
    {
        HumanDetection.onHumanNear -= SetAnimationState;
        Player.onDie -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        SetAnimationState(false);
    }
}
