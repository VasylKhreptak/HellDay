public class UI_DestroyedPhysicalObjectsText : UI_StatisticText
{
    protected override void UpdateValue()
    {
        _tmp.text = _gameStatisticObserver.statistic.DestroyedPhysicalObjects.ToString();
    }
}
