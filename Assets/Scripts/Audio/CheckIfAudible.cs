using UnityEngine;

public class CheckIfAudible : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private AudioSource _audioSource;

    [Header("Preferences")] 
    [SerializeField] private int _checkFramerate = 2;

    private Coroutine _configurableUpdate;
    
    private void Awake()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _checkFramerate, () =>
        {
            ToggleAudioSource(IsAudible());
        });
    }

    private bool IsAudible()
    {
        return _transform.ContainsTransform(_audioSource.maxDistance, _audioListener.transform);
    }

    private void ToggleAudioSource(bool isAudible)
    {
        _audioSource.enabled = isAudible;
    }
}
