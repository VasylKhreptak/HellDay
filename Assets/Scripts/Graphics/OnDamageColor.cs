using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OnDamageColor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Data")] 
    [SerializeField] private ObjectOnDamageColorData _data;

    private Sequence _seq;
    private Tween _tween;
    private Color previous;
    
    private void Awake()
    {
        _data.halfDuration = _data.Duration / 2;
        previous = _spriteRenderer.color;
    }
 
    public void ReactOnDamage()
    {
        _seq = DOTween.Sequence();

        _seq.Append(_spriteRenderer.DOColor(_data.onDamageColor,  _data.halfDuration)).SetId("FadeTween");
        _seq.Append(_spriteRenderer.DOColor(previous,  _data.halfDuration)).SetId("FadeTween");
    }

    private void OnDestroy()
    {
        _seq.Kill();
       DOTween.Kill("FadeTween");
    }
}
