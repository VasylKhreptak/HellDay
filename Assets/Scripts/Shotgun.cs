using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [Tooltip("Number of balls in shotgun weapon")] [SerializeField]
    private int _shotgunCaliber = 10;

    [SerializeField] private int _smokeDensity = 3;

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
        OnShoot.Invoke();

        for (int i = 0; i < _shotgunCaliber; i++)
        {
            SpawnBullet();
        }

        GetAmmo();

        SpawnBulletMuff();

        for (int i = 0; i < _smokeDensity; i++)
        {
            SpawnShootSmoke();
        }

        SpawnShootSparks();

        StartShootAnimation();
    }
}