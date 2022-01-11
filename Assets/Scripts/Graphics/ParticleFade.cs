using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ParticleFade : MonoBehaviour, IPooledObject
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Data")]
    [SerializeField] private ParticleFadeData _data;

    private float _currentLifetime;
    private Tween _fadeTween;


    public void OnEnable()
    {
        _currentLifetime = Random.Range(_data.MINLifetime, _data.MAXLifetime);

        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(_currentLifetime - _data.FadeDuration);

        _fadeTween = _spriteRenderer.DOFade(0, _data.FadeDuration).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        _fadeTween.Kill();
    }
}