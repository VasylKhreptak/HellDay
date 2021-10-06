using DG.Tweening;
using UnityEngine;

public class UI_SlideAnimation : MonoBehaviour
{
    public enum AnimationType
    {
        show,
        hide
    }

    [Header("References")] 
    [SerializeField] protected RectTransform _rectTransform;
    [SerializeField] protected Vector3 _offset;
    protected Vector3 _targetAnchoredPosition;
    protected Vector3 _startAnchoredPosition;

    [Header("Preferences")]
    [SerializeField] protected float _duration = 1f;
    [SerializeField] protected AnimationCurve _animationCurve;

    protected void Awake()
    {
        _targetAnchoredPosition = _rectTransform.anchoredPosition;
        _startAnchoredPosition = _targetAnchoredPosition + _offset;
    }
    
    protected void HideElementBehindScreen()
    {
        _rectTransform.anchoredPosition = _startAnchoredPosition;
    }
    
    public virtual void Animate(AnimationType animationType, float duration, float delay)
    {
        _rectTransform.DOAnchorPos(
            animationType == AnimationType.show ? _targetAnchoredPosition : _startAnchoredPosition,
            _duration).SetEase(_animationCurve).SetDelay(delay);
    }
}