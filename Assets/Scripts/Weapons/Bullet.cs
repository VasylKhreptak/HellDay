using System;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Bullet Data")]
    [SerializeField] private BulletData _data;
    
    private Tween _waitTween;
    
    public void OnEnable()
    {
        SetMovement();
    }

    private void OnDisable()
    {
        _waitTween.Kill();
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity = transform.right * _data.Speed;

        if (gameObject.activeSelf)
        {
            _waitTween = this.DOWait(_data.LifeTime).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }
}