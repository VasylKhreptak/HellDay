public class UI_KilledAnimalsText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.KilledAnimals.ToString();
    }
}
