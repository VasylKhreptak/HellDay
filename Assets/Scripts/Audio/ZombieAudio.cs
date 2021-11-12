using System.Collections;
using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    
    [Header("Preferences")] 
    [SerializeField] private float _minPlayDelay = 3f;
    [SerializeField] private float _maxPlayDelay = 6f;
    [SerializeField, Range(0, 100)] private float _walkSoundProbability = 50f;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip[] _walkAudioClips;
    [SerializeField] private AudioClip[] _biteAudioClips;

    private AudioPooler _audioPooler;
    
    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
        
        StartCoroutine(PlayWalkSoundRoutine());
    }

    private IEnumerator PlayWalkSoundRoutine()
    {
        while (true)
        {
            Probability.Execute(_walkSoundProbability, () => { PlayWalkSound(); });

            yield return new WaitForSeconds(Random.Range(_minPlayDelay, _maxPlayDelay));
        }
    }

    private void PlayWalkSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _walkAudioClips.Random(),
            _transform.position, 1f, 1f);
    }

    public void PlaBiteSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _biteAudioClips.Random(),
            _transform.position, 1f, 1f);
    }
}
