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

    public void Animate()
    {
        _scaleTween.Kill();
        
        _rectTransform.localScale = _startScale;
        _scaleTween = _rectTransform.DOScale(_targetScale, _scaleDuration).SetEase(_scaleCurve);
    }
}
