using UnityEngine;

public class UI_RandomPixelPerfectRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Data")]
    [SerializeField] private UI_RandomPixelPerfectRotationData _data;
    private void OnEnable()
    {
        SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        _transform.localRotation = 
            Quaternion.Euler(_transform.localRotation.x, _transform.localRotation.y,_data.possibleZAngles.Random());
    }
}
