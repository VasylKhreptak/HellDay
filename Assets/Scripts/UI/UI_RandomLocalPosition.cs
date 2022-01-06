using System;
using UnityEngine;

public class UI_RandomLocalPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectTransform;

    [Header("Preferences")]
    [SerializeField] private Vector3[] _localPositions;

    private void OnEnable()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        _rectTransform.localPosition = _localPositions.Random();
    }
}
