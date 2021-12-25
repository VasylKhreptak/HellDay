using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _player;
    public Transform _transform;
    [SerializeField] private GameObject _openedDoor;
    [SerializeField] private GameObject _closedDoor;
    [SerializeField] private bool _isOpened;

    [Header("Toogle Sound")]
    [SerializeField] private AudioClip _doorSound;

    private AudioPooler _audioPooler;

    private void Awake()
    {
        SetDoorState(_isOpened);
    }

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    public void SetDoorState(bool opened)
    {
        _openedDoor.SetActive(opened);
        _closedDoor.SetActive(!opened);

        _isOpened = opened;
    }

    public void ToggleDoor()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _doorSound, _transform.position,
            1f, 1f);

        SetDoorState(!_isOpened);
    }
}
