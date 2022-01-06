using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_ButtonPressAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _time = 0.5f;
    [SerializeField] private AnimationCurve _animationCurve;

    private Vector3 _rawScale;

    private void Start()
    {
        _rawScale = _transform.localScale;
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
        _transform.DOScale(targetScale, time).SetEase(animationCurve);
    }
}
