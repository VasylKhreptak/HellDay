using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private  Weapons weaponType;
    private IWeapon iWeapon;
    
    [Header("Player movement impact")]
    [ Tooltip("the percentage that will reduce staff mobility")]
    [SerializeField, Range(0, 70)] private float _movementImpact = 10f;

    public Weapons WeaponType => weaponType;
    public IWeapon Iweapon => iWeapon;
    public float MovementImpact => _movementImpact;

    
    private void Awake()
    {
        if (TryGetComponent(out IWeapon weapon))
        {
            iWeapon = weapon;
        }
    }
}
