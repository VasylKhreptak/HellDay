using UnityEngine;

public class MissileInteract : ExplosiveObjectCore
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();

        gameObject.SetActive(false);
    }
}
