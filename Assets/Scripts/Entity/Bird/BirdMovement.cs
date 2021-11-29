using DG.Tweening;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _movementSpeed = 1f;

    [Header("BirdData")] 
    [SerializeField] private BirdData _birdData;
    
    private Tween _tween;
    
    private void FixedUpdate()
    {
        _transform.Translate(new Vector3(-_movementSpeed, 0, 0));
    }
    
    private void OnEnable()
    {
        KillTweens();

            _tween = this.DOWait(_birdData.LifeTime).OnComplete(() => { gameObject.SetActive(false); });
        
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
        _movementSpeed = _birdData.IncreasedSpeed;
        this.DOWait(_birdData.LiftDuration).OnComplete(() => { _movementSpeed = normalSpeed; });
    }
}
