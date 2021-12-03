using System;
using UnityEngine;

public class PerformanceCheck : MonoBehaviour
{
    // private readonly int _iterations = 20000000;
    //
    // private void Start()
    // {
    //     ObjectPooler testReference = ObjectPooler.Instance;
    //     
    //     Debug.Log("!= null");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //         if (testReference != null) ;
    //     }));
    //     
    //     Debug.Log("!");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //         if (testReference) ;
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
