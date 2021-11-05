using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip[] _audioClips;
    
    private void OnDisable()
    {
        RandomAudio.Play(_transform.position, _audioClips);
    }
}
