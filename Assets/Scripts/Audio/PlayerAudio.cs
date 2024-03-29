using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _legKickAudioClips;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    public void PlayLegKickSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _legKickAudioClips.Random(),
            _transform.position, 1f, 1f);
    }
}