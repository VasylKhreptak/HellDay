using UnityEngine;

public class PlayerFallInteract : OnPhysicalHit
{
    [Header("Player Audio")] 
    [SerializeField] private PlayerAudio _playerAudio;
    
    private void OnEnable()
    {
        onPhysicalHit += ReactOnHit;
    }

    private void OnDisable()
    {
        onPhysicalHit -= ReactOnHit;
    }

    private void ReactOnHit()
    {
        _playerAudio.PlayBoneCrackSound();
    }
}
