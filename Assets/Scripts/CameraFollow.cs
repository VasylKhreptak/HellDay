using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private Transform _target;
    
    private void FixedUpdate()
    {
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, _target.position.x, _smoothSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, _target.position.y, _smoothSpeed * Time.deltaTime),
            transform.position.z);
    }
}
