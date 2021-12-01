using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform _transform;

    [Header("Intensity Preferences")]
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _maxSourceRange = 25f;

    private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;

    private void Awake()
    {
        _cinemachinePerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        ExplosiveObjectCore.onCameraShake += Shake;
        MissileLauncher.onCameraShake += Shake;
    }

    private void OnDisable()
    {
        ExplosiveObjectCore.onCameraShake -= Shake;
        MissileLauncher.onCameraShake -= Shake;
    }
 
    public void Shake(Vector3 source, float  maxIntensity, float duration)
    {
        if (CanShake(source) == false) return;
        
        float intensity = _curve.Evaluate(_transform.position, source,
            maxIntensity, _maxSourceRange);
        float _halfDuration = duration / 2;
        
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
