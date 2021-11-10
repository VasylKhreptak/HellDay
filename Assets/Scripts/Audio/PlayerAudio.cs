using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _transform;

    [Header("Leg Kick Audio Clips")] 
    [SerializeField] private AudioClip[] _legKickAudioClips;
    
    [Header("Bone Crack Audio Clips")]
    [SerializeField] private AudioClip[] _boneCrackAudioClips;

    public void PlayLegKickSound()
    {
        RandomAudio.Play(_transform.position, _legKickAudioClips);
    }

    public void PlayBoneCrackSound()
    {
        RandomAudio.Play(_transform.position, _boneCrackAudioClips);
    }
}
