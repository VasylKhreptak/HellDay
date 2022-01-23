using UnityEngine;

public class ScreenSleep : MonoBehaviour
{
    [SerializeField] private bool _canSleep = true;

    private void Awake()
    {
        if (_canSleep) return;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    } 
}