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
        for (int i = 0; i < _destroyParticles.Length; i++)
        {
            _objectPooler.GetFromPool(_destroyParticles[i], _transform.position, Quaternion.identity);
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
