using System;
using System.Collections;
using UnityEngine;

public class ZombieAtackCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    [SerializeField] protected ZombieAudio _audio;

    [Header("Target Detection")]
    [SerializeField] protected DamageableTargetDetection _damageableTargetDetection;

    [Header("Core Data")]
    [SerializeField] private ZombieAtackCoreData _coreData;

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
            if (CanAtack()) Atack();

            yield return new WaitForSeconds(_coreData.AtackDelay);
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
}