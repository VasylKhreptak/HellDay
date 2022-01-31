using System;
using DG.Tweening;
using UnityEngine;

public class UI_GameOverSign : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _signObject;
    [SerializeField] private UI_CanvasFadeAnimation _fadeAnimation;

    [Header("Preferences")]
    [SerializeField] private float _showDelay = 1f;

    private Tween _waitTween;

    public static Action onShow;

    private void OnEnable()
    {
        LevelFailedObserver.onLevelFailed += ShowSignWithDelay;
    }

    private void OnDisable()
    {
        LevelFailedObserver.onLevelFailed -= ShowSignWithDelay;
    }

    private void SetState(bool state)
    {
        _signObject.SetActive(state);

        if (state)
        {
            onShow?.Invoke();
            
            _fadeAnimation.Animate( true);
        }
    }
    
    private void ShowSignWithDelay()
    {
        _waitTween.Kill();

        _waitTween = this.DOWait(_showDelay).OnComplete(() =>
        {
            SetState(true);
        });
    }
}