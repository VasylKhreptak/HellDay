using System;
using DG.Tweening;
using UnityEngine;

public class UI_ScaleAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectTransform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _targetScale = Vector3.one;
    [SerializeField] private float _scaleDuration = 1f;
    [SerializeField] private AnimationCurve _scaleCurve;

    private Tween _scaleTween;

    public void Animate(bool show)
    {
        _scaleTween.Kill();

        _rectTransform.localScale = show ? _startScale : _targetScale;
        _scaleTween = _rectTransform.DOScale(show ? _targetScale : _startScale, 
            _scaleDuration).SetEase(_scaleCurve);
    }

    public static void Animate(UI_ScaleAnimation[] scaleAnimations, bool state)
    {
        foreach (var scaleAnimation in scaleAnimations)
        {
            scaleAnimation.Animate(state);
        }
    }

    private void OnDisable()
    {
        _scaleTween.Kill();
    }
}