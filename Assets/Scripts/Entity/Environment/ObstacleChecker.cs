public class ObstacleChecker : EnvironmentCheckerCore
{
    public bool isObstacleClose { get; private set; }

    protected override void OnEnterSmth()
    {
        isObstacleClose = true;
    }

    protected override void OnExitSmth()
    {
        isObstacleClose = false;
    }
}
