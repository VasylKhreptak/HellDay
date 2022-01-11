using System;
using DG.Tweening;
using UnityEngine;

public class UI_GameOverSignAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _signObject;
    [SerializeField] private UI_FadeAnimation[] _fadeAnimations;

    [Header("Preferences")]
    [SerializeField] private float _showDelay = 1f;

    private Tween _waitTween;

    public static Action onShow;

    private void OnEnable()
    {
        Player.onDie += ShowSign;
    }

    private void OnDisable()
    {
        Player.onDie -= ShowSign;
    }

    private void ShowSign()
    {
        _waitTween.Kill();

        _waitTween = this.DOWait(_showDelay).OnComplete(() =>
        {
            _signObject.SetActive(true);

            onShow?.Invoke();

            StartAnimation();
        });
    }

    private void StartAnimation()
    {
        foreach (var fadeAnimation in _fadeAnimations) fadeAnimation.Animate();
    }
}