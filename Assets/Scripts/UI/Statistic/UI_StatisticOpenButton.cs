using UnityEngine;
using UnityEngine.UI;

public class UI_StatisticOpenButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI_StatisticTab _statisticTab;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenStatisticTab);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenStatisticTab);
    }

    private void OpenStatisticTab()
    {
        _statisticTab.Show();
    }
}
