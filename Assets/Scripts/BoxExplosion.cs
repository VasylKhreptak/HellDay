using System.Collections;
using UnityEngine;

public class BoxExplosion : MonoBehaviour, IPoolSpawnedObject
{
    [SerializeField] private ParticleSystem _particleSystem;
    private float _duration;

    private void Awake()
    {
        Messenger<Pools, GameObject>.AddListener(GameEvent.POOL_OBJECT_SPAWNED, OnObjectSpawn);
        
        _duration = GetParticleDuration();
    }

    private void OnDestroy()
    {
        Messenger<Pools, GameObject>.RemoveListener(GameEvent.POOL_OBJECT_SPAWNED, OnObjectSpawn);
    }

    public void OnObjectSpawn(Pools pool, GameObject obj)
    {
        if (pool != Pools.BoxDestroyParticle || gameObject != obj) return;
        
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