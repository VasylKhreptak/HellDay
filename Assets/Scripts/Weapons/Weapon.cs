using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private  Weapons weaponType;
    private IWeapon iWeapon;
    public PlayerAmmo playerAmmo;
    public Pools weaponPoolName;
    
    [Header("Player movement impact")]
    [ Tooltip("the percentage that will reduce staff mobility")]
    [SerializeField, Range(0, 70)] private float _movementImpact = 10f;

    [Header("Damage")] 
    [SerializeField] private float _minDamage = 10f;
    [SerializeField] private float _maxDamage = 20f;
    
    public float GetDamageValue() => Random.Range(_minDamage, _maxDamage);
    
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
