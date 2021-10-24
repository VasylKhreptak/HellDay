using UnityEngine;

public class ScreenSleep : MonoBehaviour
{
    [SerializeField] private bool _enableScreenSleep = true;

    private void Awake()
    {
        if (_enableScreenSleep) return;
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
