using DG.Tweening;
using TMPro;
using UnityEngine;

public class ConsoleLog : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmp;

    [Header("Prefernces")]
    [SerializeField] private float _showTome = 3f;

    private void OnEnable()
    {
        Application.logMessageReceived += ProcessLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= ProcessLog;
    }

    private void ProcessLog(string log, string stackTrace, LogType logType)
    {
        _tmp.text += "\n* " + log;
        this.DOWait(_showTome).OnComplete(() => { _tmp.text = ""; });
    }
}
