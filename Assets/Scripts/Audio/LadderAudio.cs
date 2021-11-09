using System.Collections;
using UnityEngine;

public class LadderAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    
    [Header("Preferences")] 
    [SerializeField] private float _volume = 1f;
    [SerializeField] private float _delay;

    [Header("AudioClips")] 
    [SerializeField] private AudioClip[] _audioClips;

    private Coroutine _playAudioCoroutine;
    
    public void StartPlying()
    {
        if (_playAudioCoroutine == null)
        {
            _playAudioCoroutine = StartCoroutine(PlayAudioRoutine());
        }
    }

    public void StopPlaying()
    {
        if (_playAudioCoroutine != null)
        {
            StopCoroutine(_playAudioCoroutine);

            _playAudioCoroutine = null;
        }
    }

    private IEnumerator PlayAudioRoutine()
    {
        while (true)
        {
            RandomAudio.Play(_transform.position, _audioClips, _volume);
            
            yield return new WaitForSeconds(_delay);
        }
    }
    
    private void OnDestroy()
    {
        StopPlaying();
    }
}
