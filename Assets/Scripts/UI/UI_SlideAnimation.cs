using DG.Tweening;
using UnityEngine;

public class UI_SlideAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _time = 1f;
    [SerializeField] private float _startDelay = 0f;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] Transform _start;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = _rectTransform.position;

        HideBehindScreen();

        StartAnimating();
    }
 
    private void HideBehindScreen()
    {
        _rectTransform.position = _start.position;
    }

    private void StartAnimating()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(_startDelay);

        sequence.Append(DOTween.To(() => _rectTransform.position,
            x => _rectTransform.position = x,
            _targetPosition, _time).SetEase(_animationCurve));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_start.position, 0.5f);
        Gizmos.DrawLine(_start.position, _rectTransform.position);
    }
#endif
}