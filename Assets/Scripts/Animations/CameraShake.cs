using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _transform;

    [Header("Preferences")] 
    [SerializeField] private float _duration;

    [Header("Intensity Preferences")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _maxIntensity = 11f;
    [SerializeField] private float _maxSourceRange = 25f;

    private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
    private float _halfDuration;
    

    private void Awake()
    {
        _cinemachinePerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _halfDuration = _duration / 2;
    }

    private void OnEnable()
    {
        Messenger<Vector3>.AddListener(GameEvents.SHAKE_CAMERA, Shake);
    }

    private void OnDisable()
    {
        Messenger<Vector3>.RemoveListener(GameEvents.SHAKE_CAMERA, Shake);
    }
 
    public void Shake(Vector3 source)
    {
        if (CanShake(source) == false) return;
        
        float intensity = _curve.Evaluate(_transform.position, source,
            _maxIntensity, _maxSourceRange);
        
        _cinemachinePerlin.m_AmplitudeGain = intensity;

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append( DOTween.To(() => { return _cinemachinePerlin.m_AmplitudeGain; }, 
            x => _cinemachinePerlin.m_AmplitudeGain = x, intensity, _halfDuration));
        sequence.Append(DOTween.To(() => { return _cinemachinePerlin.m_AmplitudeGain; },
            x => _cinemachinePerlin.m_AmplitudeGain = x, 0, _halfDuration));
    }

    private bool CanShake(Vector3 source)
    {
        return _transform.position.ContainsPosition(_maxSourceRange, source);
    }

    private void OnDrawGizmosSelected()
    {
        if(_transform == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_transform.position, _maxSourceRange);
    }
}
