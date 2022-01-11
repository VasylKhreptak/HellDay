using UnityEngine;

public class PerformanceCheck : MonoBehaviour
{
    // private readonly int _iterations = 20000000;
    //
    // private delegate void test();
    //
    // private int a;
    //
    // private event test testEvent;
    //
    // private void Start()
    // {
    //     ObjectPooler testReference = ObjectPooler.Instance;
    //     
    //     Debug.Log("normal");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //         testEvent = Test;
    //         testEvent.Invoke();
    //     }));
    //     
    //     Debug.Log("anonimus");
    //     Debug.Log(GetExecutionTime(_iterations, () =>
    //     {
    //         testEvent = () => { a = 1;};
    //         testEvent.Invoke();
    //     }));
    // }
    //
    // private void Test()
    // {
    //     a = 1;
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