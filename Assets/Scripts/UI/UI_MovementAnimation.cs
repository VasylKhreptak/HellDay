using DG.Tweening;
using UnityEngine;

public class UI_MovementAnimation : MonoBehaviour
{
    [System.Serializable]
    protected enum AnimationType
    {
        appear,
        disappear
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

    protected virtual void StartAnimation(AnimationType animationType, float duration, float delay)
    {
        _rectTransform.DOAnchorPos(
            animationType == AnimationType.appear ? _targetAnchoredPosition : _startAnchoredPosition,
            _duration).SetEase(_animationCurve).SetDelay(delay);
    }
}