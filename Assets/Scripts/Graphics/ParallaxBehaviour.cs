using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _camera;

    [Header("Preferences")]
    [SerializeField, Range(0f, 1f)] private float _horizontalParallaxStrength = 0.1f;
    [SerializeField, Range(0f, 1f)] private float _verticalParallaxStrength = 0.1f;
    [SerializeField] private bool _enableHorizontalParallax = false;
    [SerializeField] private bool _enableVerticalParallax = false;

    private Vector3 _cameraPreviousPosition;

    private void Start()
    {
        _cameraPreviousPosition = _camera.position;
    }

    private void Update()
    {
        Vector3 delta = _cameraPreviousPosition - _camera.position;

        if (_enableHorizontalParallax == false)
        {
            delta.x = 0;
        }        
        if (_enableVerticalParallax == false)
        {
            delta.y = 0; 
        }

        delta.x *= _horizontalParallaxStrength;
        delta.y *= _verticalParallaxStrength;
        
        _cameraPreviousPosition = _camera.position;

        transform.position += delta;
    }
}
