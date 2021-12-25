using UnityEngine;

public class OnPhysicalHitSound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private OnPhysicalHit _onPhysicalHit;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _audioClips;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    private void OnEnable()
    {
        _onPhysicalHit.onHit += PlaySound;
    }

    private void OnDisable()
    {
        _onPhysicalHit.onHit -= PlaySound;
    }

    private void PlaySound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _audioClips.Random(), _transform.position,
            1f, 1f);
    }
}
