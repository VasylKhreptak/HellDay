using UnityEngine;

public class UI_FPSText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _container;

    private const string KEY = "ShowFPSValue";

    private void Awake()
    {
        if (PlayerPrefsSafe.GetBool(KEY, false))
        {
            _container.SetActive(true);
        }
    }
}
