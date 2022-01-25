public class UI_ExplodedFuelBarrelsText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.ExplodedFuelBarrels.ToString();
    }
}
