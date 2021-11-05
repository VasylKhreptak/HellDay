using UnityEngine;

public class Zombie : Entity, IKillable
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            TakeDamage(WeaponControl.defaultBulletDamage);
        }
    }
}