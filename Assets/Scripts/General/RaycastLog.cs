using UnityEngine;

public class RaycastLog : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        
        Touch touch = Input.GetTouch(0);
        
        Ray ray = _camera.ScreenPointToRay(touch.position);
        
        Debug.DrawRay(ray.origin, ray.direction * 1000000f, Color.green, 100f);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 1000000f))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}