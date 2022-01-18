using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCounterEvent : MonoBehaviour, IPointerDownHandler
{
    [Header("Data")]
    [SerializeField] private TouchCounterEventData _data;

    private int _count;

    public Action onReachCount;

    public void OnPointerDown(PointerEventData eventData)
    {
        _count++;

        if (_count == _data.CountEvent)
        {
            onReachCount?.Invoke();
            _count = 0;
        }
    }
}