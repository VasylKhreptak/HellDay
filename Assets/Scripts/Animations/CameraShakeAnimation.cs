using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraShakeAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _transform;

    [Header("Preferences")] 
    [SerializeField] private float _intensity;
    [SerializeField] private float _duration;

    private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;

    private void Awake()
    {
        _cinemachinePerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SHAKE_CAMERA, Shake);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SHAKE_CAMERA, Shake);
    }
 
    public void Shake()
    {
        float halfDuration = _duration / 2;
        
        _cinemachinePerlin.m_AmplitudeGain = _intensity;

        _transform.DOWait(_duration, () => { _cinemachinePerlin.m_AmplitudeGain = 0; });

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append( DOTween.To(() => { return _cinemachinePerlin.m_AmplitudeGain; }, 
            x => _cinemachinePerlin.m_AmplitudeGain = x, _intensity,
            halfDuration));
        sequence.Append(DOTween.To(() => { return _cinemachinePerlin.m_AmplitudeGain; },
            x => _cinemachinePerlin.m_AmplitudeGain = x, 0,
            halfDuration));
    }
}
