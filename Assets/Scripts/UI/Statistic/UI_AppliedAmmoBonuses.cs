using TMPro;
using UnityEngine;

public class UI_AppliedAmmoBonuses : UI_StatisticText
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmp;
    [SerializeField] private GameStatisticData _data;
    
    private void OnEnable()
    {
        UpdateValue();
    }

    public override void UpdateValue()
    {
        _tmp.text = _data.AppliedAmmoBonuses.ToString();
    }
}
