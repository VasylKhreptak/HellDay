using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnDamageColor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Data")] 
    [SerializeField] private ObjectOnDamageColorData _data;

    private float _halfDuration;
    private Sequence _seq;
    private Tween _tween;
    
    private void Awake()
    {
        _halfDuration = _data.Duration / 2;
    }
 
    public void ReactOnDamage()
    {
        _seq = DOTween.Sequence();

        _seq.Append(_spriteRenderer.DOColor(_data.onDamageColor, _halfDuration)).SetId("FadeTween");
        _seq.Append(_spriteRenderer.DOColor(Color.white, _halfDuration)).SetId("FadeTween");
    }

    private void OnDestroy()
    {
        _seq.Kill();
       DOTween.Kill("FadeTween");
    }
}
