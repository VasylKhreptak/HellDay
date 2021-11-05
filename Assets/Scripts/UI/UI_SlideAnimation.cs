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
    
    protected virtual void Start()
    {
        HideBehindScreen();
    }

    protected void Awake()
    {
        _targetAnchoredPos = _rectTransform.anchoredPosition;
        _startAnchoredPos = _targetAnchoredPos + _offset;
    }
    
    protected void HideBehindScreen()
    {
        _rectTransform.anchoredPosition = _startAnchoredPos;
    }

    protected  void SetAnimationState(bool show)
    {
        if (show && _isShown || (show == false && _isShown == false))
        {
            return;
        }
        
        Animate(show);
        _isShown = show;
    }
    
    private void Animate(bool show)
    {
        _rectTransform.DOAnchorPos(
            show ? _targetAnchoredPos : _startAnchoredPos,
            _duration).SetEase(_animationCurve);
    }
}