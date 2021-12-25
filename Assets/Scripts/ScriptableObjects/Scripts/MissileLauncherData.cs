using UnityEngine;

[CreateAssetMenu(fileName = "MissileLauncherData", menuName = "ScriptableObjects/MissileLauncherData")]
public class MissileLauncherData : ScriptableObject
{
    [Header("Preferences")]
    public Pools missile;
    [SerializeField] private float _shootDelay = 2f;

    [Header("Camera Shake")]
    [SerializeField] private float _maxCameraShakeIntensity = 2f;
    [SerializeField] private float _cameraShakeDuration = 0.4f;

    [Header("Target Detection Preferences")]
    [SerializeField] private float _checkRange = 9f;

    [Header("Audio")]
    public AudioClip _missileLaunchSound;

    public float ShootDelay => _shootDelay;
    public float MAXCameraShakeIntensity => _maxCameraShakeIntensity;
    public float CameraShakeDuration => _cameraShakeDuration;
    public float CheckRange => _checkRange;
}
