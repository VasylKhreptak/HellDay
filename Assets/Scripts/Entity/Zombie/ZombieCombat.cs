using System.Collections;
using UnityEngine;

public class ZombieCombat : MonoBehaviour
{
    [Header("Damage")] 
    [SerializeField] protected float _damage = 20f;

    [Header("Target Detection")]
    [SerializeField] protected Player _player;
    [SerializeField] protected TargetDetection _targetDetection;
    
    [Header("Preferences")] 
    [SerializeField] protected float _atackRadius;
    [SerializeField] protected Transform _atackCenter;
    [SerializeField] protected float _atackDelay = 1;
    [SerializeField] protected LayerMask _playerLayerMask;

    protected ObjectPooler _objectPooler;

    protected void Awake()
    {
        StartCoroutine(ControlAtackRoutine());
    }

    protected void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    protected virtual IEnumerator ControlAtackRoutine()
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

    protected virtual bool CanAtact()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(_atackCenter.position, _atackRadius, _playerLayerMask);

        return collider2D != null;
    }

    protected void Atack()
    {
        _player.TakeDamage(_damage);

        SpawnAtackParticles();
    }

    protected virtual void SpawnAtackParticles()
    {
        _objectPooler.GetFromPool(Pools.ZombieBiteParticle, _atackCenter.position, Quaternion.identity);
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
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