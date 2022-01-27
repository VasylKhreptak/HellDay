using System;
using DG.Tweening;
using UnityEngine;

public class UI_BillboardAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _container;
    [SerializeField] private UI_CanvasFadeAnimation _fadeAnimation;

    [Header("Preferences")]
    [SerializeField] private float _showDelay = 0.5f;

    private Tween _waitTween;
    
    private void OnEnable()
    {
        _container.SetActive(false);
        
       _waitTween = this.DOWait(_showDelay).OnComplete(() =>
        {
            _container.SetActive(true);
            _fadeAnimation.Animate(true);
        });
    }

    private void OnDisable()
    {
        _waitTween.Kill();
    }
}