using System;

public class PlayerAmmo : WeaponAmmo
{
    public static Action<string> onSetAmmoText;

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
        onSetAmmoText?.Invoke(_ammo.ToString());
    }
}