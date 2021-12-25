using UnityEngine;
using UnityEngine.AI;

public class EntityFallInteract : OnPhysicalHit
{
    [Header("references")]
    [SerializeField] private Transform _transform;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _boneCrackAudioClips;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    private void OnEnable()
    {
        onHit += ReactOnHit;
    }

    private void OnDisable()
    {
        onHit -= ReactOnHit;
    }

    private void ReactOnHit()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _boneCrackAudioClips.Random(),
            _transform.position, 1f, 1f);
    }
}
