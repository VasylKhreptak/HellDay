using System.Collections;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour, IPooledObject
{
    [SerializeField] private ParticleSystem _particleSystem;
    private float _duration;

    private void Awake()
    {
        _duration = GetParticleDuration();
    }

    public void OnEnable()
    {
        _particleSystem.Play();
        
        StartCoroutine(DisableObject(_duration));
    }
    
    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    private float GetParticleDuration()
    {
        return _particleSystem.main.startLifetime.constantMax;
    }
}