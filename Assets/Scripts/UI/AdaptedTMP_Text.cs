using TMPro;
using UnityEngine;

public class AdaptedTMP_Text : ColorAdapter
{
    [SerializeField] private TMP_Text _adaptee;

    public AdaptedTMP_Text(TMP_Text adaptee)
    {
        _adaptee = adaptee;
    }

    public override Color color
    {
        get => _adaptee.color;
        set => _adaptee.color = value;
    }
}