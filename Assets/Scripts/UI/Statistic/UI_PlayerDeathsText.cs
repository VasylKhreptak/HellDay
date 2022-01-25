public class UI_PlayerDeathsText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.Deaths.ToString();
    }
}
