using System;
using System.Collections;
using UnityEngine;

public static class ConfigurableUpdate
{
    public static void StartUpdate(MonoBehaviour owner, ref Coroutine coroutine, float framerate, Action action)
    {
        if (coroutine == null) coroutine = owner.StartCoroutine(UpdateRoutine(framerate, action));
    }

    public static void StopUpdate(MonoBehaviour owner, ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            owner.StopCoroutine(coroutine);

            coroutine = null;
        }
    }

    private static IEnumerator UpdateRoutine(float framerate, Action action)
    {
        var delay = 1 / framerate;

        while (true)
        {
            action?.Invoke();

            yield return new WaitForSeconds(delay);
        }
    }
}