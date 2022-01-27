using UnityEngine;

public class AdaptedSpriteRendererForColor : ColorAdapter
{
    [SerializeField] private SpriteRenderer _adaptee;

    public override UnityEngine.Color color
    {
        get => _adaptee.color;
        set => _adaptee.color = value;
    }
}