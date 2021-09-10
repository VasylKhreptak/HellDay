using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    private class Pool
    {
        [SerializeField] private Pools _poolType;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _size;
        [HideInInspector] public GameObject folder;

        public GameObject Prefab => _prefab;
        public int Size => _size;
        public Pools PoolType => _poolType;
    }

    [SerializeField] private Dictionary<Pools, Queue<GameObject>> _poolDictionary;
    [SerializeField] private List<Pool> _pools;

    #region singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        CreatePoolFolders();

        FillPool();
    }

    private void CreatePoolFolders()
    {
        foreach (var pool in _pools)
        {
            pool.folder = new GameObject();
            pool.folder.transform.parent = gameObject.transform;
            pool.folder.name = pool.PoolType.ToString();
        }
    }

    private void FillPool()
    {
        _poolDictionary = new Dictionary<Pools, Queue<GameObject>>();

        foreach (var pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject gameObject = Instantiate(pool.Prefab);
                gameObject.SetActive(false);

                gameObject.transform.parent = pool.folder.transform;

                objectPool.Enqueue(gameObject);
            }

            _poolDictionary.Add(pool.PoolType, objectPool);
        }
    }

    public GameObject GetFromPool(Pools pool)
    {
        if (!_poolDictionary.ContainsKey(pool))
        {
            Debug.LogWarning("Pool with name " + pool + "doesn't exist");
            return null;
        }

        GameObject objectFromPool = _poolDictionary[pool].Dequeue();

        objectFromPool.SetActive(true);

        _poolDictionary[pool].Enqueue(objectFromPool);

        return objectFromPool;
    }
}