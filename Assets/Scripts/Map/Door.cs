using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door references")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private GameObject _openedDoor;
    [SerializeField] private GameObject _closedDoor;
    [SerializeField] private bool _isOpened;

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
        SetDoorState(!_isOpened);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Messenger<UI_SlideAnimation.AnimationType>.Broadcast(GameEvent.ANIMATE_OPEN_DOOR_BUTTON,
                UI_SlideAnimation.AnimationType.show);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Messenger<UI_SlideAnimation.AnimationType>.Broadcast(GameEvent.ANIMATE_OPEN_DOOR_BUTTON,
                UI_SlideAnimation.AnimationType.hide);
        }
    }
}
