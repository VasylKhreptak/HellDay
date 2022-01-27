using UnityEngine;
using UnityEngine.UI;

public class AdaptedImageForColor : ColorAdapter
{
    [SerializeField] private Image _adaptee;

    public AdaptedImageForColor(Image adaptee)
    {
        _adaptee = adaptee;
    }

    public override UnityEngine.Color color
    {
        get => _adaptee.color;
        set => _adaptee.color = value;
    }
}