public class BarrierChecker : EnvironmentCheckerCore
{
    public bool isBarrierClose { get; private set; }

    protected override void OnEnterSmth()
    {
        isBarrierClose = true;
    }

    protected override void OnExitSmth()
    {
        isBarrierClose = false;
    }
}