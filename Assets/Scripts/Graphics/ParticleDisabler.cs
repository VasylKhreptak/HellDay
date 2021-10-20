using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour, IPooledObject
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [SerializeField] private ParticleSystem _particleSystem;
    private float _duration;

    private void Awake()
    {
        _duration = _particleSystem.main.startLifetime.constantMax;
    }

    public void OnEnable()
    {
        _particleSystem.Play();

        _transform.DOWait(_duration, () => { gameObject.SetActive(false); });
    }
}