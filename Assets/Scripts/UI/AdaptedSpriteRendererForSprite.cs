using UnityEngine;

public class AdaptedSpriteRendererForSprite : SpriteAdapter
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public AdaptedSpriteRendererForSprite(SpriteRenderer adaptee)
    {
        _spriteRenderer = adaptee;
    }

    public override Sprite sprite
    {
        get => _spriteRenderer.sprite;
        set => _spriteRenderer.sprite = value;
    }
}