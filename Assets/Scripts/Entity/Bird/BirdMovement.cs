using DG.Tweening;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Preferences")]
    [SerializeField] private float _movementSpeed = 1f;

    [Header("BirdData")]
    [SerializeField] private BirdData _birdData;

    private Tween _waitTween, _fadeTween;

    private void FixedUpdate()
    {
        _transform.Translate(new Vector3(-_movementSpeed, 0, 0));
    }

    private void OnEnable()
    {
        KillTweens();

        _spriteRenderer.color = _spriteRenderer.color.WithAlpha(1f);

        _waitTween = this.DOWait(_birdData.LifeTime).OnComplete(() => { gameObject.SetActive(false); });
        _fadeTween = this.DOWait(_birdData.LifeTime - _birdData.FadeDuration).OnComplete(() =>
        {
            _spriteRenderer.DOFade(0f, _birdData.FadeDuration);
        });

        ExplosiveObjectCore.onPlayedLoudSound += IncreaseSpeed;
        WeaponCore.onShoot += IncreaseSpeed;
    }

    private void OnDisable()
    {
        KillTweens();

        ExplosiveObjectCore.onPlayedLoudSound -= IncreaseSpeed;
        WeaponCore.onShoot -= IncreaseSpeed;
    }

    private void IncreaseSpeed()
    {
        var normalSpeed = _movementSpeed;
        _movementSpeed = _birdData.IncreasedSpeed;
        this.DOWait(_birdData.IncreaseSpeedTime).OnComplete(() => { _movementSpeed = normalSpeed; });
    }

    private void KillTweens()
    {
        _waitTween.Kill();
        _fadeTween.Kill();
    }
}