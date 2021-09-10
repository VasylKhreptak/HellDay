using System.Collections;
using UnityEngine;

public class BulletMuff : MonoBehaviour, IPoolSpawnedObject
{
    [SerializeField] private float _verticalVelocity = 1f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _horizontalVelocity = 2f;
    [SerializeField] private float _lifeTime = 2f;

    private void Awake()
    {
        Messenger<Pools, GameObject>.AddListener(GameEvent.POOL_OBJECT_SPAWNED, OnObjectSpawn);
    }

    private void OnDestroy()
    {
        Messenger<Pools, GameObject>.RemoveListener(GameEvent.POOL_OBJECT_SPAWNED, OnObjectSpawn);
    }

    public void OnObjectSpawn(Pools pool, GameObject bulletMuff)
    {
        if (pool != Pools.DefaultBulletMuff || gameObject != bulletMuff) return;

        SetMovement();

        if (gameObject.activeSelf)
            StartCoroutine(DisableObject(_lifeTime));
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity = new Vector2(Random.Range(-_horizontalVelocity, _horizontalVelocity),
            _verticalVelocity);
    }

    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}