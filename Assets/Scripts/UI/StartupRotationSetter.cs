using UnityEngine;

public class StartupRotationSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    private Quaternion _savedLocalRot;

    private void Awake()
    {
        _savedLocalRot = _transform.localRotation;
    }

    private void OnEnable()
    {
        RestoreRotation();
    }

    private void RestoreRotation()
    {
        _transform.localRotation = _savedLocalRot;
    }
}
