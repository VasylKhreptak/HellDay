using DG.Tweening;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _lifeTime = 10f;

    [Header("OnPlaeyrLoudAudio")]
    [SerializeField] private float _liftDuration = 1f;
    [SerializeField] private float _increasedSpeed = 2f;

    private Tween _tween;
    
    private void FixedUpdate()
    {
        _transform.Translate(new Vector3(-_movementSpeed, 0, 0));
    }
    
    private void OnEnable()
    {
        KillTweens();

            _tween = this.DOWait(_lifeTime).OnComplete(() => { gameObject.SetActive(false); });
        
        Messenger.AddListener(GameEvents.PLAYED_LOUD_AUDIO_SOURCE, OnPlayedLoudAudio);
    }

    private void OnDisable()
    {
        KillTweens();
    }

    private void KillTweens()
    {
        _tween.Kill();
        this.DOKill();
    }

    private void OnPlayedLoudAudio()
    {
        float normalSpeed = _movementSpeed;
        _movementSpeed = _increasedSpeed;
        this.DOWait(_liftDuration).OnComplete(() => { _movementSpeed = normalSpeed; });
    }
}
