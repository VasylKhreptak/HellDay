using DG.Tweening;
using UnityEngine;

public class OnBulletHitAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private OnBulletHitEvent _onBulletHitEvent;

    [Header("Data")] 
    [SerializeField] private OnBulletHitAudioData _data;

    private Tween _tween;
    private bool _canPlay = true;
    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    private void OnEnable()
    {
        _onBulletHitEvent.onHit += PlaySound;
    }

    private void OnDisable()
    {
        _onBulletHitEvent.onHit -= PlaySound;
    }

    private void PlaySound(Collision2D other)
    {
        if (_canPlay)
        {
            _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data.audioClips.Random(), 
                other.transform.position, 1f, 1f);

            ControlPlaySpeed();
        }
    }

    private void ControlPlaySpeed()
    {
        _tween.Kill();
        _canPlay = false;
        this.DOWait(_data.MINDelay).OnComplete(() => { _canPlay = true; });
    }
}
