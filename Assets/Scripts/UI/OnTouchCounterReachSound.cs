using UnityEngine;

public class OnTouchCounterReachSound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TouchCounterEvent _touchCounterEvent;
    [SerializeField] private Transform _transform;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] _audioClips;

    private AudioPooler _audioPooler;
    
    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    private void OnEnable()
    {
        _touchCounterEvent.onReachCount += PlaySound;
    }

    private void OnDisable()
    {
        _touchCounterEvent.onReachCount -= PlaySound;
    }

    private void PlaySound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _audioClips.Random(), _transform.position,
            1f, 1f);
    }
}
