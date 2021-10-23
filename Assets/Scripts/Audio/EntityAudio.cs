using UnityEngine;

public class EntityAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected Transform _transform;
    
    [Header("Audio Clips")]
    [SerializeField] protected AudioClip[] _deathAudioClips;
    
    public void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathAudioClips[Random.Range(0, _deathAudioClips.Length)], 
            _transform.position);
    }
}
