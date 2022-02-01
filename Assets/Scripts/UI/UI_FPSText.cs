using UnityEngine;

public class UI_FPSText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _container;

    [Header("PlayerPrefs Preferences")]
    [SerializeField] private string _playerPrefsKey;
    [SerializeField] private bool _defaultState;

    private void Awake()
    {
        if (PlayerPrefsSafe.GetBool(_playerPrefsKey, _defaultState))
        {
            _container.SetActive(true);
        }
    }
}
