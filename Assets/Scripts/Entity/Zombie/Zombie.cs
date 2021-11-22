using UnityEngine;

public class Zombie : DamageableObject
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            TakeDamage(PlayerWeaponControl.defaultBulletDamage);
        }
    }
}