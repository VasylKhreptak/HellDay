using TMPro;
using UnityEngine;

public class UI_RandomText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _TMP;

    [Space(10)]
    [SerializeField, TextArea] private string[] _textVariants;
    
    private void OnEnable()
    {
        _TMP.text = _textVariants.Random();
    }

}
