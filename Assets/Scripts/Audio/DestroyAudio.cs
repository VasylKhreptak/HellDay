using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip[] _audioClips;
    
    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
        {
            RandomAudio.Play(_transform.position, _audioClips);
        }
    }
}
