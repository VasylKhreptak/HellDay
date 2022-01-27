using System;
using DG.Tweening;
using UnityEngine;

public class UI_CanvasFadeAnimation : UI_FadeAnimation
{
    [Header("References")]
    [SerializeField] private CanvasGroup _canvasGroup;

    public override void Animate(bool show)
    {
        _fadeTween.Kill();

        _canvasGroup.alpha = show ? _startAlpha : _targetAlpha;

        _fadeTween = DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x,
            show ? _targetAlpha : _startAlpha, _fadeDuration).SetEase(_fadeCurve);
    }
}