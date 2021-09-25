using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class ParticleMovement : MonoBehaviour, IPooledObject
{
    [Header("References")] [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("HorizontalMovement")] [SerializeField]
    private float _minHorizontalVelocity = 2f;

    [SerializeField] private float _maxHorizontalVelocity = 5f;

    [Header("VerticalMovement")] [SerializeField]
    private float _minVerticalVelocity = 1f;

    [SerializeField] private float _maxVerticalVelocity = 3f;

    [Header("Lifetime")] [SerializeField] private float _minLifetime = 5f;
    [SerializeField] private float _maxLifetime = 8f;

    [Header("Preferences")] [SerializeField]
    private float _fadeDuration = 1f;

    private float _currentLifetime;

    public void OnEnable()
    {
        SetRandomLifetime();

        StartCoroutine(StartMovement());
    }

    private void OnDisable()
    {
        DOTween.Kill(gameObject);

        Color spriteColor = _spriteRenderer.color;
        spriteColor.a = 1;
    }

    private IEnumerator StartMovement()
    {
        _rigidbody2D.velocity = new Vector2(Random.Range(_minHorizontalVelocity, _maxHorizontalVelocity),
            Random.Range(_minVerticalVelocity, _maxVerticalVelocity));

        yield return new WaitForSeconds(_currentLifetime - _fadeDuration);

        _spriteRenderer.DOFade(0, _fadeDuration).OnComplete(() => gameObject.SetActive(false));
    }

    private void SetRandomLifetime()
    {
        _currentLifetime = Random.Range(_minLifetime, _maxLifetime);
    }
}