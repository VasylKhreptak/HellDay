public class PitChecker : EnvironmentCheckerCore
{
    public bool isPitNearp { get; private set; }

    protected override void OnEnterSmth()
    {
        isPitNearp = false;
    }

    protected override void OnExitSmth()
    {
        isPitNearp = true;
    }
}