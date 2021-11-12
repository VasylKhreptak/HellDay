using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Pools[] _destroyParticles;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void SpawnDestroyParticles()
    {
        foreach (var particle in _destroyParticles)
        {
            _objectPooler.GetFromPool(particle, _transform.position, Quaternion.identity);
        }
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
        {
            SpawnDestroyParticles();
        }
    }
}
