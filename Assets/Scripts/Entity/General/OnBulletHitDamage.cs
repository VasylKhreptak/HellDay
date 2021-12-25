using UnityEngine;

public class OnBulletHitDamage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;
    [SerializeField] private OnBulletHitEvent _onBulletHitEvent;

    private PlayerWeaponControl _playerWeaponControl;

    private void Start()
    {
        _playerWeaponControl = GameAssets.Instance.playerWeaponControl;
    }

    private void OnEnable()
    {
        _onBulletHitEvent.onHit += ReactOnBullet;
    }

    private void OnDisable()
    {
        _onBulletHitEvent.onHit -= ReactOnBullet;
    }

    private void ReactOnBullet(Collision2D other)
    {
        _damageableObject.TakeDamage(_playerWeaponControl.currentWeapon.GetDamageValue());
    }
}
