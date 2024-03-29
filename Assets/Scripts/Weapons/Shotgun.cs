using UnityEngine;

public class Shotgun : WeaponCore
{
    [Tooltip("Number of balls in shotgun weapon")]
    [SerializeField] private int _shotgunCaliber = 10;

    protected override void ShootActions()
    {
        _audioSource.Play();

        for (var i = 0; i < _shotgunCaliber; i++) SpawnBullet();

        _playerAmmo.GetAmmo();
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.TriggerShootAnimation(_animator, ShootTrigger);
    }
}