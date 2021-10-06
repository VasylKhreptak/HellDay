public class DoorDestroy : DestroyableItem
{
    protected override void DestroyActions()
    {
        Destroy(_transform.parent.gameObject);
        
        base.DestroyActions();
    }
}
