using UnityEngine;

public class ScreenSleep : MonoBehaviour
{
    [SerializeField] private bool _enableScreenSleep = true;

    private void Awake()
    {
        if(_enableScreenSleep == true) return;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
