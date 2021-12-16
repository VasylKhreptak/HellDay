using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour, IPooledObject
{
    [Header("References")] 
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Preferences")] 
    [SerializeField] private float _additionalDelay;
    
    private float _duration;

    private Tween _waitTween;
    
    private void Awake()
    {
        _duration = _particleSystem.main.startLifetime.constantMax;
    }

    public void OnEnable()
    {
        _particleSystem.Play();

        _waitTween.Kill();
        _waitTween = this.DOWait(_duration + _additionalDelay).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false) return;

        _waitTween.Kill();
    }
}