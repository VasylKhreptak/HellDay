using DG.Tweening;
using UnityEngine;

public class UI_ColorFadeAnimation : UI_FadeAnimation
{
    [Header("References")]
    [SerializeField] private ColorAdapter _colorAdapter;

    public override void Animate(bool show)
    {
        Debug.Log("Color adapter == null: " + (_colorAdapter == null));
        
        _fadeTween.Kill();
        
        _colorAdapter.color = _colorAdapter.color.WithAlpha(show ? _startAlpha : _targetAlpha);

        _fadeTween = DOTween.To(() => _colorAdapter.color.a,
            x => _colorAdapter.color = _colorAdapter.color.WithAlpha(x),
            show ? _targetAlpha : _startAlpha, _fadeDuration).SetEase(_fadeCurve);
    }

    public static void Animate(UI_ColorFadeAnimation[] fadeAnimations, bool state)
    {
        foreach (var fadeAnimation in fadeAnimations)
        {
            fadeAnimation.Animate(state);
        }
    }
}