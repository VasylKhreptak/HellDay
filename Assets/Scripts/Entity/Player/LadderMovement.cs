using TMPro.EditorUtilities;
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

    public static bool isOnLadder { get; private set; } = false;

    private GameObject _currentLadder;

    private void Update()
    {
        if (isOnLadder == true)
        {
            if(_joystick.Vertical > _joystickVerticalSensetivty)
            {
                _rigidbody2D.velocity =
                new Vector2(0, _movementSpeed);

                AllignToLadder();
                
                return;
            }
            else if (_joystick.Vertical < -_joystickVerticalSensetivty)
            {
                return;
            }

            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void AllignToLadder()
    {
        if (_currentLadder.activeSelf == false)
        {
            return;
        }

        float positionX = Mathf.Lerp(_playerTransform.position.x,
            _currentLadder.transform.position.x,
            _alligementSpeed);

        _playerTransform.position =
            new Vector3(positionX, _playerTransform.position.y,
                _playerTransform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder")  == true)
        {
            isOnLadder = true;

            _currentLadder = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") == true)
        {
            isOnLadder = false;

            _currentLadder = null;
        }
    }
}
