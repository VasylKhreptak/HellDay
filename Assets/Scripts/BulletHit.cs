using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private void Awake()
    {
        Messenger<Collision2D>.AddListener(GameEvent.BULLET_HIT, OnBulletHit);
    }

    private void OnDestroy()
    {
        Messenger<Collision2D>.RemoveListener(GameEvent.BULLET_HIT, OnBulletHit);
    }

    private void OnBulletHit(Collision2D other)
    {
    }
}
