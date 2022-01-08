using UnityEngine;
using UnityEngine.UI;
public class AdaptedImage : ColorAdapter
{
    [SerializeField] private Image _adaptee;

    public AdaptedImage(Image adaptee)
    {
        this._adaptee = adaptee;
    }

    public override Color color { get { return _adaptee.color;} set { _adaptee.color = value; } }
}
