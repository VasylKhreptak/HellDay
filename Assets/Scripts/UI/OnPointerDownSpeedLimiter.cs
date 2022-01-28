using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerDownSpeedLimiter : MonoBehaviour, IPointerDownHandler
{
    [Header("Handlers")]
    [SerializeField] private MonoBehaviour[] _handlerScripts;

    [Header("Preferences")]
    [SerializeField] private float _delay = 1f;

    private bool _canTouch = true;

    private Tween _waitTween;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_canTouch == false)
            return;

        _canTouch = false;
        
        SetHandlersState(false);

        _waitTween.Kill();
        _waitTween = this.DOWait(_delay).OnComplete(() =>
        {
            _canTouch = true;
            SetHandlersState(true);
        });
    }

    private void SetHandlersState(bool state)
    {
        foreach (var handler in _handlerScripts)
        {
            handler.enabled = state;
        } 
    }
    
    private void OnDisable()
    {
        _waitTween.Kill();
    }
}