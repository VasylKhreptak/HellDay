using System;
using DG.Tweening;
using UnityEngine;

public class UI_LevelScrollAreaAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI_CanvasFadeAnimation _fadeAnimation;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Preferences")]
    [SerializeField] private float _startupDelay = 2f;

    private Tween _waitTween;

    private void OnEnable()
    {
        _canvasGroup.alpha = 0f;
        
        this.DOWait(_startupDelay).OnComplete(() => { _fadeAnimation.Animate(true); });
    }

    private void OnDisable()
    {
        _waitTween.Kill();
    }
}