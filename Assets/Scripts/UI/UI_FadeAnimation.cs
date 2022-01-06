using DG.Tweening;
using UnityEngine;

public class UI_FadeAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Preferences")]
    [SerializeField, Range(0f, 1f)] private float _startAlpha = 0f;
    [SerializeField, Range(0f, 1f)] private float _targetAlpha = 1f;
    [SerializeField] private float _fadeDuration = 0.7f;
    [SerializeField] private AnimationCurve _fadeCurve;

    private Tween _fadeTween;

    public void Animate()
    {
        _fadeTween.Kill();

        _spriteRenderer.color = _spriteRenderer.color.WithAlpha(_startAlpha);
        
        _fadeTween = _spriteRenderer.DOFade(_targetAlpha, _fadeDuration).SetEase(_fadeCurve);
    }

}
