public class UI_TotalUsedAmmoText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.TotalUsedAmmo.ToString();
    }
}
