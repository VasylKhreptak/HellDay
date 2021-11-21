using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthBonusItem : BonusItemCore
{
    [Header("Preferences")] 
    [SerializeField] private float _minHealth = 10f;
    [SerializeField] private float _maxHealth = 40f;

    [Header("Apply effect")]
    [SerializeField] private Pools _applyEffect;
    
    private Player _player;
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected override void OnCollisionWithPlayer(Collision2D player)
    {
        if (_player == null)
        {
            player.gameObject.TryGetComponent(out _player);
        }

        if (_player && _player.gameObject.activeSelf && 
            Mathf.Approximately(_player.health, _player.maxHealth) == false)
        {
            _player.SetHealth(_player.health + Random.Range(_minHealth, _maxHealth));

            SpawnHealthSpellEffect(_player);
            
            gameObject.SetActive(false);
        }
    }

    private void SpawnHealthSpellEffect(Player player)
    {
        GameObject healSpell = _objectPooler.GetFromPool(_applyEffect, 
            player.transform.position, Quaternion.identity);

        healSpell.transform.parent = player.transform;
    }
}
