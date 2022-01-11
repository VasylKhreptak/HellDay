using UnityEngine;

[CreateAssetMenu(fileName = "ParticleFadeData", menuName = "ScriptableObjects/ParticleFadeData")]
public class ParticleFadeData : ScriptableObject
{
    [Header("Lifetime")]
    [SerializeField] private float _minLifetime = 20f;
    [SerializeField] private float _maxLifetime = 30f;

    [Header("Preferences")]
    [SerializeField] private float _fadeDuration = 1f;

    public float MINLifetime => _minLifetime;
    public float MAXLifetime => _maxLifetime;
    public float FadeDuration => _fadeDuration;
}