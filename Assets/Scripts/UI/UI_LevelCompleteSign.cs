using System;
using DG.Tweening;
using UnityEngine;

public class UI_LevelCompleteSign : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _container;
    [SerializeField] private UI_CanvasFadeAnimation _fadeAnimations;

    [Header("Preferences")]
    [SerializeField] private float _showDelay = 1f;

    public static Action onShow;

    private Tween _waitTween;

    private void OnEnable()
    {
        LevelCompleteObserver.onLevelComplete += ShowSign;
    }

    private void OnDisable()
    {
        LevelCompleteObserver.onLevelComplete -= ShowSign;

        _waitTween.Kill();
    }

    private void ShowSign()
    {
        _waitTween.Kill();
        this.DOWait(_showDelay).OnComplete(() =>
        {
            onShow?.Invoke();

            _container.SetActive(true);
            _fadeAnimations.Animate(true);
        });
    }
}