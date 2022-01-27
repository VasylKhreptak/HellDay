using TMPro;
using UnityEngine;

public class AdaptedTMP_TextForColor : ColorAdapter
{
    [SerializeField] private TMP_Text _adaptee;

    public AdaptedTMP_TextForColor(TMP_Text adaptee)
    {
        _adaptee = adaptee;
    }

    public override UnityEngine.Color color
    {
        get => _adaptee.color;
        set => _adaptee.color = value;
    }
}