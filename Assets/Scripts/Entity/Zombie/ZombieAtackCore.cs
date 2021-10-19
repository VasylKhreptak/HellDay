using System;
using System.Collections;
using UnityEngine;

public class ZombieAtackCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    
    [Header("Damage")] 
    [SerializeField] protected float _damage = 20f;

    [Header("Target Detection")]
    [SerializeField] protected KillableTargetDetection  _killableTargetDetection;
    
    [Header("Preferences")]
    [SerializeField] protected float _atackDelay = 1;
    
    protected ObjectPooler _objectPooler;

    protected void Start()
    {
        StartCoroutine(ControlAtackRoutine());
        
        _objectPooler = ObjectPooler.Instance;
    }

    protected virtual IEnumerator ControlAtackRoutine()
    {
        while (true)
        {
            if (CanAtack())
            {
                Atack();
            }

            yield return new WaitForSeconds(_atackDelay);
        }
    }

    protected virtual bool CanAtack()
    {
        throw new NotImplementedException();
    }

    protected virtual void Atack()
    {
        throw new NotImplementedException();
    }
    
#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (_killableTargetDetection.ClosestTarget?.gameObject.activeSelf == true)
        {
            Gizmos.DrawWireCube(_killableTargetDetection.ClosestTarget.Transform.position,
                Vector2.one);

            Gizmos.DrawLine(_transform.position,
                _killableTargetDetection.ClosestTarget.Transform.position);
        }
    }
#endif
}