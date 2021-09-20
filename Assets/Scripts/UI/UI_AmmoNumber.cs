using System;
using TMPro;
using UnityEngine;

public class UI_AmmoNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text _TMPText;


    private void Awake()
    {
        Messenger<string>.AddListener(GameEvent.SET_AMMO_TEXT, SetText);
    }

    private void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvent.SET_AMMO_TEXT, SetText);
    }

    private void SetText(string text)
    {
        _TMPText.text = text;
    }
}