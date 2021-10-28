using System.Collections;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour, IPooledObject
{
    [SerializeField] private ParticleSystem _particleSystem;
    private float _duration;

    private void Awake()
    {
        _duration = _particleSystem.main.startLifetime.constantMax;
    }

    public void OnEnable()
    {
        _particleSystem.Play();

        this.DOWait(_duration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}