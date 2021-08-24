using System;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;

    private void OnValidate()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}