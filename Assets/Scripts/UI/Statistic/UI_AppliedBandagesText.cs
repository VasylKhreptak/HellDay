public class UI_AppliedBandagesText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.AppliedBandages.ToString();
    }
}
