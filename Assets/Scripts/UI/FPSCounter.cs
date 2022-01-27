using System.Collections;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmp;
    
    [Header("Preferences")]
    [SerializeField] private float _updateDelay = 0.5f;
    
    private Coroutine _updateCoroutine;

    private void OnEnable()
    {
        StartUpdating();
    }

    private void OnDisable()
    {
        StopUpdating();
    }

    private void StartUpdating()
    {
        if (_updateCoroutine == null)
        {
            _updateCoroutine = StartCoroutine(UpdateRoutine());
        }
    }

    private void StopUpdating()
    {
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);

            _updateCoroutine = null;
        }
    }

    private IEnumerator UpdateRoutine()
    {
        while (true)
        {
            UpdateValue();
            
            yield return new WaitForSeconds(_updateDelay);
        }
    }

    private void UpdateValue()
    {
        _tmp.text = ((int)(1f / Time.deltaTime)).ToString();
    }
}
