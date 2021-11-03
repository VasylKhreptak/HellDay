using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door references")] 
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _transform;
    [SerializeField] private GameObject _openedDoor;
    [SerializeField] private GameObject _closedDoor;
    [SerializeField] private bool _isOpened;
    [SerializeField] private AudioSource _audioSource;

    public Transform Transform => _transform;
    
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
        if (other.CompareTag("Player"))
        {
            if(_player.activeSelf)
            {
                Messenger<UI_SlideAnimation.AnimationType>.Broadcast(GameEvent.ANIMATE_OPEN_DOOR_BUTTON,
                UI_SlideAnimation.AnimationType.show);
            }
        }
        else if (other.CompareTag("Human"))
        {
            ToggleDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Messenger<UI_SlideAnimation.AnimationType>.Broadcast(GameEvent.ANIMATE_OPEN_DOOR_BUTTON,
                UI_SlideAnimation.AnimationType.hide);
        }
        else if (other.CompareTag("Human"))
        {
            ToggleDoor();
        }
    }
}
