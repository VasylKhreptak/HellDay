using System.Text;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _playerTransform;
    
    [Header("Preferences")]
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField, Range(0f, 1f)] private float _joystickVerticalSensetivty = 0.8f;
    [SerializeField] private float _alligementSpeed = 3f;

    public static bool isOnLadder { get; private set; }
    public static bool isClimbing { get; private set; }

    private GameObject _currentLadder;

    private void Update()
    {
        if (isOnLadder)
        {
            if(IsMovingUp())
            {
                _rigidbody2D.velocity =
                new Vector2(0, _movementSpeed);

                isClimbing = true;
                
                AllignToLadder();
            }
            else if (IsMovingDown())
            {
                isClimbing = true;
                
                AllignToLadder();
            }
            else if (IsStaying())
            {
                isClimbing = false;
                
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            }
        }
        else
        {
            isClimbing = false;
        }
    }

    private bool IsMovingUp()
    {
        return _joystick.Vertical > _joystickVerticalSensetivty;
    }

    private bool IsMovingDown()
    {
        return _joystick.Vertical < -_joystickVerticalSensetivty;
    }

    private bool IsStaying()
    {
        return IsMovingDown() == false && IsMovingUp() == false;
    }

    private void AllignToLadder()
    {
        if (_currentLadder.activeSelf == false) return;

        float positionX = Mathf.Lerp(_playerTransform.position.x,
            _currentLadder.transform.position.x,
            _alligementSpeed);

        _playerTransform.position =
            new Vector3(positionX, _playerTransform.position.y,
                _playerTransform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;

            _currentLadder = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;

            _currentLadder = null;
        }
    }
}
