using System.Collections;
using UnityEngine;

public class LadderAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _delay;

    [Header("AudioClips")]
    [SerializeField] private AudioClip[] _audioClips;

    private Coroutine _playAudioCoroutine;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    public void StartPlying()
    {
        if (_playAudioCoroutine == null) _playAudioCoroutine = StartCoroutine(PlayAudioRoutine());
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
            _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _audioClips.Random(),
                _transform.position, 1f, 1f);

            yield return new WaitForSeconds(_delay);
        }
    }

    private void OnDestroy()
    {
        StopPlaying();
    }
}
