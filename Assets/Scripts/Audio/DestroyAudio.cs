using System;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _audioClips;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
            _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _audioClips.Random(), _transform.position,
                1f, 1f);
    }
}
