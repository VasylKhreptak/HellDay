using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Data")]
    [SerializeField] private ZombieAudioData _data;


    private AudioPooler _audioPooler;
    private uint _audioID;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;

        StartCoroutine(PlayWalkSoundRoutine());
    }

    private IEnumerator PlayWalkSoundRoutine()
    {
        while (true)
        {
            Probability.Execute(_data.MoveSoundProbability, () => { PlayWalkSound(); });

            yield return new WaitForSeconds(Random.Range(_data.MINPlayDelay, _data.MAXPlayDelay));
        }
    }

    private void PlayWalkSound()
    {
        _audioID = _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data.moveAudioClips.Random(),
            _transform.position, 1f, 1f);
    }

    public void PlaBiteSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data.biteAudioClips.Random(),
            _transform.position, 1f, 1f);
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded == false) return;

        _audioPooler.StopOneShootSound(_audioID);
    }
}
