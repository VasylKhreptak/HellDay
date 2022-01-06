using UnityEngine;

public class GyroscopeGravityAssigner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    private float  _gravityForce;
    
    private Gyroscope gyro;
    
    private void Awake()
    {
        gyro = Input.gyro;

        _gravityForce = 9.81f * _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
    }

    private void OnEnable()
    {
        gyro.enabled = true;
    }

    private void OnDisable()
    {
        gyro.enabled = false;
    }
    
    private void FixedUpdate()
    {
        Vector2 gravityDir = new Vector2(gyro.gravity.x, -1);
        
        _rigidbody2D.AddForce(gravityDir * _gravityForce);
    }
}
