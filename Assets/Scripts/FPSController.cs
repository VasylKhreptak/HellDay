using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;
    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
