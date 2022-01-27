using System;
using DG.Tweening;
using UnityEngine;

public class UI_GameOverSign : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _signObject;
    [SerializeField] private UI_ColorFadeAnimation[] _fadeAnimations;

    [Header("Preferences")]
    [SerializeField] private float _showDelay = 1f;

    private Tween _waitTween;

    public static Action onShow;

    private void OnEnable()
    {
        Player.onDie += ShowSignWithDelay;
    }

    private void OnDisable()
    {
        Player.onDie -= ShowSignWithDelay;
    }

    private void SetState(bool state)
    {
        _signObject.SetActive(state);

        if (state)
        {
            onShow?.Invoke();
            
            UI_ColorFadeAnimation.Animate(_fadeAnimations, true);
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