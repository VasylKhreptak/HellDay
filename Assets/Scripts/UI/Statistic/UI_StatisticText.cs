using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_StatisticText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TMP_Text _tmp;

    protected GameStatisticObserver _gameStatisticObserver;
    
    protected virtual void OnEnable()
    {
        _gameStatisticObserver = GameStatisticObserver.Instance;
        
        UpdateValue();
    }

    protected virtual void UpdateValue()
    {
        throw new NotImplementedException();
    }
}