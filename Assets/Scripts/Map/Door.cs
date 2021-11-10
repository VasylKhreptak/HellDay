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

    public void SetDoorState(bool opened)
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
}
