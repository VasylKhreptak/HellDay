public class PlayerAmmo : WeaponAmmo
{
    protected override void Awake()
    {
        SetAmmo(_startupAmmo);
    }

    protected void OnEnable()
    {
        UpdateAmmoText();
    }

    public void SetAmmoWithTextUpdate(int ammo)
    {
        SetAmmo(ammo);

        UpdateAmmoText();
    }

    public override void GetAmmo()
    {
        base.GetAmmo();
        
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        Messenger<string>.Broadcast(GameEvents.SET_AMMO_TEXT, 
            _ammo.ToString(), MessengerMode.DONT_REQUIRE_LISTENER);
    }
}
