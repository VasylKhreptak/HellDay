using UnityEngine;

public class HumanAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Resque AudioClips")] 
    [SerializeField] private AudioClip[] _resqueAudioClips;

    public void PlayResqueSound()
    {
        RandomAudio.Play(_audioSource, _resqueAudioClips);
    }
}
