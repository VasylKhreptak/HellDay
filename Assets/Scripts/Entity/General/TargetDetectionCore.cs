using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetectionCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;

    [Header("Data")]
    [SerializeField] protected TargetDetectionData _data;


    protected virtual void Awake()
    {
        StartCoroutine(FindClosestTargetRoutine());
    }

    protected virtual IEnumerator FindClosestTargetRoutine()
    {
        yield return null;
    }
}
