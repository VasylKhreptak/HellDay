using UnityEngine;

public class OnBulletHit : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private DamageableObject _damageableObject;
    
    [Header("Preferences")] 
    [SerializeField] private LayerMask _bulletLayerMask;

    private PlayerWeaponControl _playerWeaponControl;

    private void Start()
    {
        _playerWeaponControl = PlayerWeaponControl.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_bulletLayerMask.ContainsLayer(other.gameObject.layer))
        {
            _damageableObject.TakeDamage(_playerWeaponControl.currentWeapon.GetDamageValue());
        }
    }
}
