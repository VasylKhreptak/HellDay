using System;
using DG.Tweening;
using UnityEngine;

public class ResqueSignAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _signTransform;
    [SerializeField] private SpriteRenderer _signRenderer;

    [Header("Preferences")] 
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private float _duration;
    
    [Header("Curves")] 
    [SerializeField] private AnimationCurve _moveCurve;
    [SerializeField] private AnimationCurve _alphaCurve;
    [SerializeField] private AnimationCurve _scaleCurve;

    private bool _isShown;

    private void Awake()
    {
        Color color = _signRenderer.color;
        color.a = 0;
        _signRenderer.color = color;

        _signTransform.localScale = _start.localScale;

        _signTransform.localPosition = _start.localPosition;
    }

    public void SetSignState(bool state)
    {
        if (state && _isShown || (state == false && _isShown == false))
        {
            return;
        }

        ShowSign(state);
        _isShown = state;
    }
    
    private void ShowSign(bool state)
    {
        _signTransform.DOLocalMove(state ? _end.localPosition : _start.localPosition, _duration).SetEase(_moveCurve);
        _signTransform.DOScale(state ? _end.localScale : _start.localScale, _duration).SetEase(_scaleCurve);
        _signRenderer.DOFade(Convert.ToSingle(state), _duration).SetEase(_alphaCurve);
    }

    private void OnDestroy()
    {
        _signTransform.DOKill();
        _signRenderer.DOKill();
        
    }
}
