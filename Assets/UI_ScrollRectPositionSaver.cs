using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScrollRectPositionSaver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect _scrollRect;

    [Header("Preferences")]
    [SerializeField] private string _key;

    private void Start()
    {
        _scrollRect.horizontalNormalizedPosition = PlayerPrefsSafe.GetFloat(_key, 0f);
    }

    private void OnDestroy()
    {
        PlayerPrefsSafe.SetFloat(_key, _scrollRect.horizontalNormalizedPosition);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefsSafe.SetFloat(_key, _scrollRect.horizontalNormalizedPosition);
        }
    }
}