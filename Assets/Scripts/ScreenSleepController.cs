using UnityEngine;

public class ScreenSleepController : MonoBehaviour
{
    [SerializeField] private bool _enableScreenSleep = true;

    private void Awake()
    {
        if(!_enableScreenSleep) return;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
