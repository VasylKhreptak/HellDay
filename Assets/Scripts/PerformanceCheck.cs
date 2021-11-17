using System;
using UnityEngine;

public class PerformanceCheck : MonoBehaviour
{
    // private readonly int _iterations = 1;
    //
    // private void Start()
    // {
    //     Debug.Log("tag");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //
    //     }));
    //     
    //     Debug.Log("component");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //         
    //     }));
    // }
    //
    // private float GetExecutionTime(int iterations, Action action)
    // {
    //     float _startTime, _endTime;
    //     
    //     _startTime = Time.realtimeSinceStartup;
    //
    //     for (int i = 0; i < iterations; i++)
    //     {
    //         action.Invoke();
    //     }
    //
    //     _endTime = Time.realtimeSinceStartup;
    //
    //     return _endTime - _startTime;
    // }
}
