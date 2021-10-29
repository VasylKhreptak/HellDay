using UnityEngine;

public class DestroyAudio : RandomAudioCore
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip[] _audioClips;
    
    private void OnDisable()
    {
        PlayRandomAudio(_transform.position, _audioClips);
    }
}
