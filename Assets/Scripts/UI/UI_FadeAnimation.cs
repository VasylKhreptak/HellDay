using System;
using DG.Tweening;
using UnityEngine;

public class UI_FadeAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ColorAdapter _colorAdapter;

    [Header("Preferences")]
    [SerializeField] [Range(0f, 1f)] private float _startAlpha = 0f;
    [SerializeField] [Range(0f, 1f)] private float _targetAlpha = 1f;
    [SerializeField] private float _fadeDuration = 0.7f;
    [SerializeField] private AnimationCurve _fadeCurve;

    private Tween _fadeTween;

    public void Animate(bool show)
    {
        _fadeTween.Kill();

        _colorAdapter.color = _colorAdapter.color.WithAlpha(show ? _startAlpha : _targetAlpha);

        _fadeTween = DOTween.To(() => _colorAdapter.color.a,
            x => _colorAdapter.color = _colorAdapter.color.WithAlpha(x),
            show ? _targetAlpha : _startAlpha, _fadeDuration).SetEase(_fadeCurve);
    }

    public static void Animate(UI_FadeAnimation[] fadeAnimations, bool state)
    {
        foreach (var fadeAnimation in fadeAnimations)
        {
            fadeAnimation.Animate(state);
        }
    }

    private void OnDisable()
    {
        _fadeTween.Kill();
    }
}