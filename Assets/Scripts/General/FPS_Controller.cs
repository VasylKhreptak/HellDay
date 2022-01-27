using UnityEngine;

public class FPS_Controller : MonoBehaviour
{
    [SerializeField] private int _defaultTargetFrameRate = 60;

    private const string KEY = "FramerateValue";
    
    private void Start()
    {
        SetTargetFrameRate(PlayerPrefsSafe.GetInt(KEY, _defaultTargetFrameRate));
    }

    public void SetTargetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
        QualitySettings.vSyncCount = 0;
    }
}