using System;
using UnityEngine;

public class HumanAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Resque AudioClips")]
    [SerializeField] private AudioClip[] _resqueAudioClips;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    public void PlayResqueSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _resqueAudioClips.Random(), _transform.position,
            1f, 1f);
    }
}