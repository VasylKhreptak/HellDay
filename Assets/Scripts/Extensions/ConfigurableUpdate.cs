using System;
using System.Collections;
using UnityEngine;

public class ConfigurableUpdate : MonoBehaviour
{
    private Coroutine _configurableUpdate = null;

    public void StartUpdate(int framerate, Action action)
    {
        if (_configurableUpdate == null)
        {
            _configurableUpdate = StartCoroutine(UpdateRoutine(framerate, action));
        }
    }

    public void StopUpdate()
    {
        StopCoroutine(_configurableUpdate);

        _configurableUpdate = null;
    }

    private IEnumerator UpdateRoutine(int framerate, Action action)
    {
        float delay = 1 / framerate;

        while (true)
        {
            action.Invoke();
            
            yield return new WaitForSeconds(delay);
        }
    }
}
