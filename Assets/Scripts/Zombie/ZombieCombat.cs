using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieCombat : MonoBehaviour
{
    [Header("Damage")] [SerializeField] private float _damage = 20f;

    [Header("References")] [SerializeField]
    private Transform _target;

    [SerializeField] private ZombieAIMovement _zombieAIMovement;

    [SerializeField] private Player _player;

    [Header("Preferences")] [SerializeField]
    private float _atackRadius;

    [SerializeField] private Transform _atackCenter;
    [SerializeField] private float _atackDelay = 1;
    [SerializeField] private LayerMask _playerLayerMask;

    private ObjectPooler _objectPooler;

    private void Awake()
    {
        StartCoroutine(ControlAtackRoutine());
    }

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private IEnumerator ControlAtackRoutine()
    {
        while (true)
        {
            if (CanAtact() == true)
            {
                Atack();
            }

            yield return new WaitForSeconds(_atackDelay);
        }
    }

    private bool CanAtact()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(_atackCenter.position, _atackRadius, _playerLayerMask);

        return collider2D != null;
    }


    private void Atack()
    {
        _player.TakeDamage(_damage);

        SpawnZombieBiteParticle();
    }

    private void SpawnZombieBiteParticle()
    {
        _objectPooler.GetFromPool(Pools.ZombieBiteParticle, _atackCenter.position, Quaternion.identity);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_atackCenter == null)
        {
            return;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_atackCenter.position, _atackRadius);
    }
#endif
}