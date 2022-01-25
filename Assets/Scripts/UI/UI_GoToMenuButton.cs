using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GoToMenuButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(LoadMenu);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(LoadMenu);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
