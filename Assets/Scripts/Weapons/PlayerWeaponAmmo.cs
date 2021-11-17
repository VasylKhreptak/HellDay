public class PlayerWeaponAmmo : WeaponAmmo
{
    protected override void SetAmmo(int ammo)
    {
        base.SetAmmo(ammo);
        
        Messenger<string>.Broadcast(GameEvents.SET_AMMO_TEXT, _ammo.ToString());
    }

    public override void GetAmmo()
    {
        base.GetAmmo();
        
        Messenger<string>.Broadcast(GameEvents.SET_AMMO_TEXT, _ammo.ToString());
    }
}
