using DG.Tweening;
using UnityEngine;

public class UI_Tab : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _container;
    [SerializeField] private UI_CanvasFadeAnimation _fadeAnimation;
    [SerializeField] private UI_ScaleAnimation _scaleAnimation;
    
    [Header("Preferences")]
    [SerializeField] private float _disableDelay = 0.3f;

    private Tween _disableTween;

    private bool _isShown;
    
    public void Show()
    {
        if (_isShown && _container.activeSelf)
            return;
        
        _isShown = true;

        _container.SetActive(true);
        
        _fadeAnimation.Animate(true);
        _scaleAnimation.Animate(true);
    }

    public void Hide()
    {
        if (_isShown == false) return;
        
        _isShown = false;

        _fadeAnimation.Animate(false);
        _scaleAnimation.Animate(false);
        
        _disableTween = this.DOWait(_disableDelay).OnComplete(() => { _container.SetActive(false); });
    }

    private void OnDestroy()
    {
        _disableTween.Kill();
    }
}