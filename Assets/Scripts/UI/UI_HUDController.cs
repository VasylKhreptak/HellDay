using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI_SlideAnimation[] _slideAnimations;
    [SerializeField] private GraphicRaycaster _hudRaycaster;
    
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
        LevelFailedObserver.onLevelFailed += HideHUDElements;
        LevelCompleteObserver.onLevelComplete += HideHUDElements;
    }

    private void OnDisable()
    {
        LevelFailedObserver.onLevelFailed -= HideHUDElements;
        LevelCompleteObserver.onLevelComplete -= HideHUDElements;
    }

    private void ShowHUDElements()
    {
        foreach (var slideAnimation in _slideAnimations)
        {
            slideAnimation.Show();

            _hudRaycaster.enabled = true;
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

                _hudRaycaster.enabled = false;
            }
        });
    }

    private void OnDestroy()
    {
        _waitTween.Kill();
    }
}