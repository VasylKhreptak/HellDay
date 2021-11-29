using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    private class Pool
    {
        public Pools _poolType;
        public GameObject _prefab;
        public int _size;
        [HideInInspector] public GameObject folder;
    }
    
    private Dictionary<Pools, Queue<GameObject>> _poolDictionary;
    [SerializeField] private List<Pool> _pools;

    
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
        
        CreatePoolFolders();

        FillPool();
    }

    
    private void CreatePoolFolders()
    {
        foreach (var pool in _pools)
        {
            pool.folder = new GameObject(pool._poolType.ToString());
            pool.folder.transform.parent = gameObject.transform;
        }
    }

    private void FillPool()
    {
        _poolDictionary = new Dictionary<Pools, Queue<GameObject>>();

        for (int i = 0; i < _pools.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < _pools[i]._size; j++)
            {
                GameObject gameObject = Instantiate(_pools[i]._prefab);
                gameObject.SetActive(false);

                gameObject.transform.parent = _pools[i].folder.transform;

                objectPool.Enqueue(gameObject);
            }

            _poolDictionary.Add(_pools[i]._poolType, objectPool);
        }
    }

    public GameObject GetFromPool(Pools pool, Vector2 Position, Quaternion Rotation)
    {
        if (_poolDictionary.ContainsKey(pool) == false)
        {
            Debug.LogWarning("Pool with name " + pool + "doesn't exist");
            return null;
        }
        
        GameObject objectFromPool = _poolDictionary[pool].Dequeue();
        
        objectFromPool.transform.position = Position;
        objectFromPool.transform.rotation = Rotation;

        if (objectFromPool.activeSelf)
        {
            objectFromPool.SetActive(false);
        }
        
        objectFromPool.SetActive(true);

        _poolDictionary[pool].Enqueue(objectFromPool);

        return objectFromPool;
    }
}