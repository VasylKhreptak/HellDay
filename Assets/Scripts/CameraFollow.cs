using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _tolerance = 0.01f;

    private void FixedUpdate()
    {
        if (IsApproximately(transform.position.x, _target.position.x, _tolerance) &&
            IsApproximately(transform.position.y, _target.position.y, _tolerance))
        {
            return;
        }

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, _target.position.x, _smoothSpeed),
            Mathf.Lerp(transform.position.y, _target.position.y, _smoothSpeed),
            transform.position.z);
    }

    private bool IsApproximately(float a, float b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a - b) < tolerance;
    }
}