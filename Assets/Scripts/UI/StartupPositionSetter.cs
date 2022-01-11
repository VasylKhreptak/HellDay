using UnityEngine;

public class StartupPositionSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    private Vector3 _savedLocalPos;

    private void Awake()
    {
        _savedLocalPos = _transform.localPosition;
    }

    private void OnEnable()
    {
        RestorePosition();
    }

    private void RestorePosition()
    {
        _transform.localPosition = _savedLocalPos;
    }
}