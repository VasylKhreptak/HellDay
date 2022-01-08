using System;
using DG.Tweening;
using UnityEngine;

public class UI_SlideAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected Vector3 _offset;
    protected Vector3 _targetAnchoredPos;
    protected Vector3 _startAnchoredPos;

    [Header("Preferences")]
    [SerializeField] protected float _duration = 1f;
    [SerializeField] protected AnimationCurve _animationCurve;

    protected bool _isShown;

    private Tween _moveTween;

    protected void Awake()
    {
        _targetAnchoredPos = _rectTransform.anchoredPosition;
        _startAnchoredPos = _targetAnchoredPos + _offset;
    }

    protected virtual void Start()
    {
        HideBehindScreen();
    }

    protected void HideBehindScreen()
    {
        _rectTransform.anchoredPosition = _startAnchoredPos;
    }

    public void Show()
    {
        if (_isShown) return;
        
        _moveTween.Kill();
        _moveTween = _rectTransform.DOAnchorPos(_targetAnchoredPos, _duration).SetEase(_animationCurve);

        _isShown = true;
    }

    public void Hide() 
    {
        if (_isShown == false) return;

        _moveTween.Kill();
        _moveTween = _rectTransform.DOAnchorPos(_startAnchoredPos, _duration).SetEase(_animationCurve);

        _isShown = false;
    }

    private void OnDisable()
    {
        _moveTween.Kill();
    }
}
