using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraShakeAnimation : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CinemachineVirtualCamera _camera;

    [Header("Preferences")] 
    [SerializeField] private float _duration;

    private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
    private float _halfDuration;
    

    private void Awake()
    {
        _cinemachinePerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _halfDuration = _duration / 2;
    }

    private void OnEnable()
    {
        Messenger<float>.AddListener(GameEvents.SHAKE_CAMERA, Shake);
    }

    private void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvents.SHAKE_CAMERA, Shake);
    }
 
    public void Shake(float intensity)
    {
        _cinemachinePerlin.m_AmplitudeGain = intensity;
        
        this.DOWait(_duration).OnComplete(() =>
        {
            _cinemachinePerlin.m_AmplitudeGain = 0;
        });

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append( DOTween.To(() => { return _cinemachinePerlin.m_AmplitudeGain; }, 
            x => _cinemachinePerlin.m_AmplitudeGain = x, intensity, _halfDuration));
        sequence.Append(DOTween.To(() => { return _cinemachinePerlin.m_AmplitudeGain; },
            x => _cinemachinePerlin.m_AmplitudeGain = x, 0, _halfDuration));
    }
}
