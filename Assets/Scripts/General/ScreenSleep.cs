using UnityEngine;

public class ScreenSleep : MonoBehaviour
{
    [SerializeField] private bool _canSleep = true;

    private void Awake()
    {
        if (_canSleep == false) return;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}