using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponBonusBox : MonoBehaviour
{
    [Serializable]
    private class LootItem
    {
        public Pools weapon;
        public float weight;
    }

    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Weapons")]
    [SerializeField] private LootItem[] _lootItems;

    private ObjectPooler _objectPooler;
    private float _totalWeight;
    private float _randomNum;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;

        foreach (var item in _lootItems) _totalWeight += item.weight;
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded == false) return;

        DropRandomWeapon();
    }

    private void DropRandomWeapon()
    {
        _randomNum = Random.Range(0, _totalWeight);

        foreach (var item in _lootItems)
            if (_randomNum <= item.weight)
            {
                _objectPooler.GetFromPool(item.weapon, _transform.position, Quaternion.identity);

                return;
            }
            else
            {
                _randomNum -= item.weight;
            }
    }
}