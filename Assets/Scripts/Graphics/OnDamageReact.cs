using DG.Tweening;
using UnityEngine;

public class OnDamageReact : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Preferences")] 
    [SerializeField] private Color _onDamageColor;
    [SerializeField] private float _duration = 0.4f;

    private float _halfDuration;
    private Sequence _seq;
    private Tween _tween;
    
    private void Awake()
    {
        _halfDuration = _duration / 2;
    }
 
    public void ReactOnHit()
    {
        _seq = DOTween.Sequence();

        _seq.Append(_spriteRenderer.DOColor(_onDamageColor, _halfDuration)).SetId("FadeTween");
        _seq.Append(_spriteRenderer.DOColor(Color.white, _halfDuration)).SetId("FadeTween");
    }

    private void OnDestroy()
    {
        _seq.Kill();
        DOTween.Kill("FadeTween");
    }
}
