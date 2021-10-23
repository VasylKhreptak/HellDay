public class PlayerAudio : EntityAudio
{
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PLAYER_DIED, PlayDeathSound);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DIED, PlayDeathSound);
    }
}
