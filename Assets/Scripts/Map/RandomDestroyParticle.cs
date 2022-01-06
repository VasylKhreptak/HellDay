using UnityEngine;

public class RandomDestroyParticle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Data")]
    [SerializeField] private RandomDestroyParticleData _data;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false) return;

        SpawnRandomDestroyParticles();
    }

    private void SpawnRandomDestroyParticles()
    {
        foreach (var particle in _data.destroyParticles)
            Probability.Execute(particle.probability, () =>
            {
                _objectPooler.GetFromPool(particle.particle, _transform.position, Quaternion.identity);
            });
    }
}
