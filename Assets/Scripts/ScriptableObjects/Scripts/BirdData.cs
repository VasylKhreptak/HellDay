using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/BirdData")]
public class BirdData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _increaseSpeedTime = 3f;
    [SerializeField] private float _incresedSpeed;
    [SerializeField] private float _fadeDuration = 1f;

    public float LifeTime => _lifeTime;
    public float IncreaseSpeedTime => _increaseSpeedTime;
    public float IncreasedSpeed => _incresedSpeed;
    public float FadeDuration => _fadeDuration;
}
