using UnityEngine;

public class Weapon : MonoBehaviour
{
    public IWeapon iWeapon;
    public Weapons weaponType;

    private void Awake()
    {
        iWeapon = GetComponent<IWeapon>();
    }
}
