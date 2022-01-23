using System.Collections;
using TMPro;
using UnityEngine;

public class UI_TotalPlayTimeText : UI_StatisticText
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmp;
    [SerializeField] private GameStatisticData _data;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay = 60f;
    
    private Coroutine _updateValueCoroutine;
    
    private void OnEnable()
    {
        StartUpdatingValue();
    }

    private void OnDisable()
    {
        StopUpdatingValue();
    }

    public override void UpdateValue()
    {
        _tmp.text = (_data.PlayTime / (60 * 60)).ToString();
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
    
            yield return new WaitForSeconds(_updateDelay);
        }
    }

}
