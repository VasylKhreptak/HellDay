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
        get { return _image.overrideSprite; }
        set
        {
            _image.overrideSprite = value;
            _image.SetNativeSize();
        }
    }
}