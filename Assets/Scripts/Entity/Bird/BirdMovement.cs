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
        _tween.Kill();
        _tween = this.DOWait(_birdData.LifeTime).OnComplete(() => { gameObject.SetActive(false); });

        ExplosiveObjectCore.onPlayedLoudSound += OnPlayedLoudAudio;
        WeaponCore.onShoot += OnPlayedLoudAudio;
    }

    private void OnDisable()
    {
        _tween.Kill();
        
        ExplosiveObjectCore.onPlayedLoudSound -= OnPlayedLoudAudio;
        WeaponCore.onShoot -= OnPlayedLoudAudio;
    }

    private void OnPlayedLoudAudio()
    {
        float normalSpeed = _movementSpeed;
        _movementSpeed = _birdData.IncreasedSpeed;
        this.DOWait(_birdData.LiftDuration).OnComplete(() => { _movementSpeed = normalSpeed; });
    }
}
