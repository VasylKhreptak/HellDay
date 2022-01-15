using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UI_HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI_SlideAnimation[] _slideAnimations;

    [Header("Preferences")]
    [SerializeField] private float _startupShowDelay = 0.5f;
    [SerializeField] private float _hideDelay = 1f;

    private Tween _waitTween;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startupShowDelay);

        ShowHUDElements();
    }

    private void OnEnable()
    {
        Player.onDie += HideHUDElements;
    }

    private void OnDisable()
    {
        Player.onDie -= HideHUDElements;
    }

    private void ShowHUDElements()
    {
        foreach (var slideAnimation in _slideAnimations)
        {
            slideAnimation.Show();
        }
    }

    private void HideHUDElements()
    {
        _waitTween.Kill();
        _waitTween = this.DOWait(_hideDelay).OnComplete(() =>
        {
            foreach (var slideAnimation in _slideAnimations)
            {
                slideAnimation.Hide();
            }
        });
    }

    private void OnDestroy()
    {
        _waitTween.Kill();
    }
}