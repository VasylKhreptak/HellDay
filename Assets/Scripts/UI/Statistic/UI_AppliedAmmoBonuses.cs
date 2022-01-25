public class UI_AppliedAmmoBonuses : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.AppliedAmmoBonuses.ToString();
    }
}
