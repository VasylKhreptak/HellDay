using UnityEngine;

public class FPS_Settings : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;

    private void Start()
    {
        SetTargetFrameRate(_targetFrameRate);
    }

    public void SetTargetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
        QualitySettings.vSyncCount = 0;
    }
}