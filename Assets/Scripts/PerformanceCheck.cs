using UnityEngine;

public class PerformanceCheck : MonoBehaviour
{
    // private readonly int _iterations = 2000;
    //
    // private void Start()
    // {
    //     Debug.Log("normal");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //         PlayerPrefsSafe.SetString("test", "123");
    //         PlayerPrefsSafe.GetString("test");
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