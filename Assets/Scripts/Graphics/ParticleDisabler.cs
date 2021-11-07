using DG.Tweening;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour, IPooledObject
{
    [SerializeField] private ParticleSystem _particleSystem;
    private float _duration;

    private Tween _tween;
    
    private void Awake()
    {
        _duration = _particleSystem.main.startLifetime.constantMax;
    }

    public void OnEnable()
    {
        _particleSystem.Play();
        _tween = this.DOWait(_duration).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}