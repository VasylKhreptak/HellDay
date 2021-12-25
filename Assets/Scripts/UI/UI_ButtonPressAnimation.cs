using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_ButtonPressAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _time = 0.5f;
    [SerializeField] private AnimationCurve _animationCurve;

    private Vector3 _rawScale;

    private void Start()
    {
        _rawScale = _rectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateButton(_targetScale, _time, _animationCurve);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AnimateButton(_rawScale, _time, _animationCurve);
    }

    private void AnimateButton(Vector3 targetScale, float time, AnimationCurve animationCurve)
    {
        DOTween.To(() => _rectTransform.localScale, x => _rectTransform.localScale = x, targetScale,
            time).SetEase(animationCurve);
    }
}
