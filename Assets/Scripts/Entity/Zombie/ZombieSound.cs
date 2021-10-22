using System.Collections;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private AudioSource _audioSource;

    [Header("Preferences")] 
    [SerializeField] private float _minDelay = 3f;
    [SerializeField] private float _maxDelay = 6f;
    [SerializeField, Range(0, 100)] private float _walkSoundProbability = 50f;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip[] _walkAudioClips;
    [SerializeField] private AudioClip[] _biteAudioClips;

    private void Awake()
    {
        StartCoroutine(PlayWalkSoundRoutine());
    }

    private IEnumerator PlayWalkSoundRoutine()
    {
        while (true)
        {
            if (_audioSource.isPlaying == false)
            {
                Probability.Execute(_walkSoundProbability, () =>
                {
                    PlayWalkSound();
                });
            }

            yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
        }
    }

    private void PlayWalkSound()
    {
        PlayRandomSound(_walkAudioClips);
    }

    public void PlaBiteSound()
    {
        PlayRandomSound(_biteAudioClips);
    }

    private void PlayRandomSound(AudioClip[] audioClips)
    {
        _audioSource.clip = audioClips[Random.Range(0, _walkAudioClips.Length)];
        _audioSource.Play();
    }
}
