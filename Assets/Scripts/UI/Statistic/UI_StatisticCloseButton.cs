using UnityEngine;
using UnityEngine.UI;

public class UI_StatisticCloseButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI_StatisticTab _statisticTab;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(CloseStatisticTab);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(CloseStatisticTab);
    }

    private void CloseStatisticTab()
    {
        _statisticTab.Hide();
    }
}
