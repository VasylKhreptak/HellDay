using System;
using UnityEngine;

public class PerformanceCheck : MonoBehaviour
{
    private void Start()
    {
        
        // Debug.Log("");
        // Debug.Log(GetExecutionTime(100000, () =>
        // {
        // }));
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
