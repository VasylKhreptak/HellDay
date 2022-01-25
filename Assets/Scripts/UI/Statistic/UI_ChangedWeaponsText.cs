public class UI_ChangedWeaponsText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.ChangedWeapons.ToString();
    }
}