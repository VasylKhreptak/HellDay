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
    
    private void Awake()
    {
        _halfDuration = _duration / 2;
    }
 
    public void ReactOnHit()
    {
        Sequence seq = DOTween.Sequence();

        seq.SetId("DamageReact");
        seq.Append(_spriteRenderer.DOColor(_onDamageColor, _halfDuration).SetId("DamageReact"));
        seq.Append(_spriteRenderer.DOColor(Color.white, _halfDuration).SetId("DamageReact"));
    }

    private void OnDestroy()
    {
        DOTween.Kill("DamageReact");
    }
}
