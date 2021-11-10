using System;
using UnityEngine;

public class PerformanceCheck : MonoBehaviour
{
    private void Start()
    {
        // Debug.Log("Distance");
        // Debug.Log(GetExecutionTime(1000000, () =>
        // {
        //     if (My2(transform, transform, 10)) ;
        // }));
        // Debug.Log("Magnitude");
        // Debug.Log(GetExecutionTime(1000000, () =>
        // {
        //     if (My1(transform, transform, 10)) ;
        // }));
        
    }

    private bool My2(Transform first, Transform second, float radius)
    {
        return Vector3.Distance(first.position, second.position) <= radius;
    }
    
    private bool My1(Transform first, Transform second, float radius)
    {
        return (first.position - second.position).sqrMagnitude < radius * radius;
    }

    
    
    
    
    
    
    
    
    
    
    private float GetExecutionTime(int iterations, Action action)
    {
        float _startTime, _endTime;
        
        _startTime = Time.realtimeSinceStartup;

        for (int i = 0; i < iterations; i++)
        {
            action.Invoke();
        }

        _endTime = Time.realtimeSinceStartup;

        return _endTime - _startTime;
    }
}
