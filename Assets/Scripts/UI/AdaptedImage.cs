using UnityEngine;
using UnityEngine.UI;

public class AdaptedImage : ColorAdapter
{
    [SerializeField] private Image _adaptee;

    public AdaptedImage(Image adaptee)
    {
        _adaptee = adaptee;
    }

    public override Color color
    {
        get => _adaptee.color;
        set => _adaptee.color = value;
    }
}