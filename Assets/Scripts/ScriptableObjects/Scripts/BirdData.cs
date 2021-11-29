using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/BirdData")]
public class BirdData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _liftDuration = 1f;
    [SerializeField] private float _incresedSpeed;

    public float LifeTime => _lifeTime;
    public float LiftDuration => _liftDuration;
    public float IncreasedSpeed => _incresedSpeed;
}
