using TMPro;
using UnityEngine;

public class UI_AmmoNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text _TMPText;
    
    private void OnEnable()
    {
        PlayerAmmo.onSetAmmoText += SetText;
    }

    private void OnDestroy()
    {
        PlayerAmmo.onSetAmmoText -= SetText;
    }

    private void SetText(string text)
    {
        _TMPText.text = text;
    }
}