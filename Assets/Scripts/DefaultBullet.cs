using System.Collections;
using UnityEngine;

public class DefaultBullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _bulletSpeed = 3;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _lifeTime = 2f;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OBJECT_SPAWNED, OnObjectSpawn);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJECT_SPAWNED, OnObjectSpawn);
    }

    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        _rigidbody2D.velocity = transform.right * _bulletSpeed;

        if (gameObject.activeSelf)
            StartCoroutine(DisableObject(_lifeTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }
}