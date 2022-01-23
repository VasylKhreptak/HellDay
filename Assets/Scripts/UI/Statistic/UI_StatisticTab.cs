using DG.Tweening;
using UnityEngine;

public class UI_StatisticTab : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _container;
    [SerializeField] private UI_FadeAnimation[] _fadeAnimations;
    [SerializeField] private UI_ScaleAnimation[] _scaleAnimations;

    [Header("Preferences")]
    [SerializeField] private float _disableDelay = 0.3f;

    private Tween _disableTween;

    private bool _isShown;

    private void SetAnimationState(bool state)
    {
        UI_FadeAnimation.Animate(_fadeAnimations, state);

        UI_ScaleAnimation.Animate(_scaleAnimations, state);
    }

    public void Show()
    {
        if (_isShown)
            return;

        _isShown = true;

        _container.SetActive(true);

        SetAnimationState(true);
    }

    public void Hide()
    {
        if (_isShown == false)
            return;

        _isShown = false;

        SetAnimationState(false);

        _disableTween = this.DOWait(_disableDelay).OnComplete(() => { _container.SetActive(false); });
    }

    private void OnDestroy()
    {
        _disableTween.Kill();
    }
}