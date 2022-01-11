using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnTakeDamageColor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Data")]
    [SerializeField] private ObjectOnDamageColorData _data;

    private Sequence _seq;
    private Color _previousColor;

    private void Awake()
    {
        _previousColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _damageableObject.onTakeDamage += ReactOnDamage;
    }

    private void OnDisable()
    {
        _damageableObject.onTakeDamage -= ReactOnDamage;
    }

    public void ReactOnDamage(float damage)
    {
        _seq.Kill();

        _seq = DOTween.Sequence();
        _seq.Append(_spriteRenderer.DOColor(_data.onDamageColor, _data.HalfDuration));
        _seq.Append(_spriteRenderer.DOColor(_previousColor, _data.HalfDuration));
    }

    private void OnDestroy()
    {
        _seq.Kill();
    }
}