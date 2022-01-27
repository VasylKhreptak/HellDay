using UnityEngine;
using UnityEngine.UI;

public class AdaptedImageForSprite : SpriteAdapter
{
    [Header("References")]
    [SerializeField] private Image _image;

    public AdaptedImageForSprite(Image adaptee)
    {
        _image = adaptee;
    }

    public override Sprite sprite
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }
}