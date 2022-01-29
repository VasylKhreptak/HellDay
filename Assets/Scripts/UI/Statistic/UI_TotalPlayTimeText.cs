using System;
using System.Collections;
using UnityEngine;

public class UI_TotalPlayTimeText : UI_StatisticText
{
    [Header("Preferences")]
    [SerializeField] private float _updateDelay = 10f;
    [SerializeField] private int _precision = 2;
    
    private Coroutine _updateValueCoroutine;

    protected override void OnEnable()
    {
        _gameStatisticObserver = GameStatisticObserver.Instance;
        
        StartUpdatingValue();
    }

    private void OnDisable()
    {
        StopUpdatingValue();
    }

    protected override void UpdateValue()
    {
        _tmp.text = (_gameStatisticObserver.statistic.PlayTime / (60f*60f)).ToString("F" + (_precision));
    }

    private void StartUpdatingValue()
    {
        if (_updateValueCoroutine == null)
        {
            _updateValueCoroutine = StartCoroutine(UpdateVaueRoutine());
        }
    }

    private void StopUpdatingValue()
    {
        if (_updateValueCoroutine != null)
        {
            StopCoroutine(_updateValueCoroutine);

            _updateValueCoroutine = null;
        }
    }

    private IEnumerator UpdateVaueRoutine()
    {
        while (true)
        {
            UpdateValue();
    
            yield return new WaitForSecondsRealtime(_updateDelay);
        }
    }

}
