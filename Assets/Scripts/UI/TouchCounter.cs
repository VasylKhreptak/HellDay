using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCounter : MonoBehaviour, IPointerDownHandler
{
    [Header("Preferences")]
    [SerializeField] private int _countEvent;
    
    private int _count;

    public static Action onReachCount;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _count++;

        if (_count == _countEvent)
        {
            onReachCount?.Invoke();
            _count = 0;
            
            Debug.Log("Touches reached: " + (_countEvent));
        }
    }
}
