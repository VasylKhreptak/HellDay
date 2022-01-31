using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LoadSceneButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _button;

    [Header("Preferences")]
    [SerializeField, Scene] private string _scene;

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
        SceneManager.LoadScene(_scene);
    }
}
