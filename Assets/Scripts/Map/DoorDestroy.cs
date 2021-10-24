public class DoorDestroy : DestroyableObject
{
    public override void DestroyActions()
    {
        Destroy(_transform.parent.gameObject);
        
        base.DestroyActions();
    }
}
