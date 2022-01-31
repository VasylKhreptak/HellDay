using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedObserver : MonoBehaviour
{
    public static Action onLevelFailed;

    private void OnEnable()
    {
        Player.onDie += OnLevelFailed;
    }

    private void OnDisable()
    {
        Player.onDie -= OnLevelFailed;
    }

    private void OnLevelFailed()
    {
        onLevelFailed?.Invoke();
    }
}
