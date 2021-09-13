using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [Tooltip("Number of balls in shotgun weapon")] [SerializeField]
    protected int _shotGunCaliber = 10;

    [SerializeField] private int _smokeDensity = 3;

    protected override IEnumerator Shoot()
    {
        while (true)
        {
            OnShoot.Invoke();

            for (int i = 0; i < _shotGunCaliber; i++)
            {
                SpawnBullet();
            }

            SpawnBulletMuff();

            for (int i = 0; i < _smokeDensity; i++)
            {
                SpawnShootSmoke();
            }

            _animator.SetTrigger(IsShooting);

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }
}