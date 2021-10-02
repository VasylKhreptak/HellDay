using System.Collections;
using UnityEngine;

public class ZombieCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    
    [Header("Damage")] 
    [SerializeField] protected float _damage = 20f;

    [Header("Target Detection")]
    [SerializeField] protected KillableTargetDetection  _killableTargetDetection;
    
    [Header("Preferences")] 
    [SerializeField] protected float _atackRadius;
    [SerializeField] protected Transform _atackCenter;
    [SerializeField] protected float _atackDelay = 1;
    [SerializeField] protected LayerMask _killableEntityLayerMask;

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
        if (_killableTargetDetection._closestKillableTarget._transform.gameObject.activeSelf == false)
        {
            return false;
        }
        
        Collider2D collider2D = Physics2D.OverlapCircle(_atackCenter.position, _atackRadius, _killableEntityLayerMask);

        return collider2D != null;
    }

    protected void Atack()
    {
        _killableTargetDetection._closestKillableTarget._Killable.TakeDamage(_damage);
        
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
        
        //_closestKillableTarget
        Gizmos.color = Color.red;
        if (_killableTargetDetection._closestKillableTarget != null)
        {
            Gizmos.DrawWireCube(_killableTargetDetection._closestKillableTarget._transform.position,
                Vector2.one);

            Gizmos.DrawLine(_transform.position,
                _killableTargetDetection._closestKillableTarget._transform.position);
        }
    }
#endif
}