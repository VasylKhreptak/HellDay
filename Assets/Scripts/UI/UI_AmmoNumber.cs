using System;
using TMPro;
using UnityEngine;

public class UI_AmmoNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text _TMPText;
    
    private void OnEnable()
    {
        Messenger<string>.AddListener(GameEvents.SET_AMMO_TEXT, SetText);
    }

    private void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvents.SET_AMMO_TEXT, SetText);
    }

    private void SetText(string text)
    {
        _TMPText.text = text;
    }
}