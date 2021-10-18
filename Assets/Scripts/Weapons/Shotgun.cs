using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Shotgun : WeaponCore
{
    [Tooltip("Number of balls in shotgun weapon")] 
    [SerializeField] private int _shotgunCaliber = 10;

    protected override IEnumerator Shoot()
    {
        while (true)
        {
            if (_canShoot == true)
            {
                ShootActions();
            }

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    protected override void ShootActions()
    {
        _audioSource.Play();

        for (int i = 0; i < _shotgunCaliber; i++)
        {
            SpawnBullet();
        }

        GetAmmo();
        
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletSpawnPlace.position, Quaternion.identity);
        
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _shootParticleSpawnPlace.position, Quaternion.identity);
        
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _shootParticleSpawnPlace.position, Quaternion.identity);
        
        _weaponVFX.StartShootAnimation(_animator, ShootTrigger);
    }
}