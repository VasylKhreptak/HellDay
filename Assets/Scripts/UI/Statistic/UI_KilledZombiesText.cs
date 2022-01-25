public class UI_KilledZombiesText : UI_StatisticText
{ 
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.KilledZombies.ToString();
    }
}
