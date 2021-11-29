using System;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Bullet Data")] 
    [SerializeField] private BulletData _data;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void OnEnable()
    {   
        SetMovement();
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity = transform.right * _data.Speed;

        if (gameObject.activeSelf)
        {
            this.DOWait(_data.LifeTime).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }
}