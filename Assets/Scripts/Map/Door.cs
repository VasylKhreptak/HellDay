using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject _player;
    public Transform _transform;
    [SerializeField] private GameObject _openedDoor;
    [SerializeField] private GameObject _closedDoor;
    [SerializeField] private bool _isOpened;
    [SerializeField] private AudioSource _audioSource;
    
    private void Awake()
    {
        SetDoorState(_isOpened);
    }

    private void SetDoorState(bool opened)
    {
        _openedDoor.SetActive(opened);
        _closedDoor.SetActive(!opened);

        _isOpened = opened;
    }

    public void ToggleDoor()
    {
        _audioSource.Play();
        
        SetDoorState(!_isOpened);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _player.activeSelf)
        {
            Messenger<bool>.Broadcast(GameEvents.ANIMATE_OPEN_DOOR_BUTTON,true);
        }
        else if (other.CompareTag("Human"))
        {
            SetDoorState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Messenger<bool>.Broadcast(GameEvents.ANIMATE_OPEN_DOOR_BUTTON,false);
        }
        else if (other.CompareTag("Human"))
        {
            SetDoorState(false);
        }
    }
}
