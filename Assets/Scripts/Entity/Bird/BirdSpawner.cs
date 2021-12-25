using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _spawnPlace;
    [SerializeField] private Transform _birdParent;

    [Header("Preferences")]
    [SerializeField] private Pools[] _birds;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _minYOffset;
    [SerializeField] private float _maxYOffset;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnBird();

            yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));
        }
    }

    private void SpawnBird()
    {
        var spawnPosition = _spawnPlace.position +
                            new Vector3(0, Random.Range(-_minYOffset, _maxYOffset), 0);

        var bird = _objectPooler.GetFromPool(_birds.Random(),
            spawnPosition, Quaternion.identity);

        bird.transform.parent = _birdParent;
    }

    private void OnDrawGizmosSelected()
    {
        if (_spawnPlace == null) return;

        Vector3 above, below;

        above = _spawnPlace.position + new Vector3(0, _maxYOffset, 0);
        below = _spawnPlace.position - new Vector3(0, _minYOffset, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(above - new Vector3(40, 0, 0), above);
        Gizmos.DrawLine(below - new Vector3(40, 0, 0), below);
        Gizmos.DrawLine(above, below);
    }
}
