using UnityEngine;

[CreateAssetMenu(fileName = "ZombieAudioData", menuName = "ScriptableObjects/ZombieAudioData")]
public class ZombieAudioData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _minPlayDelay = 3f;
    [SerializeField] private float _maxPlayDelay = 6f;
    [SerializeField] [Range(0f, 100f)] private float _moveSoundProbability = 50f;

    [Header("Audio")]
    [Space]
    public AudioClip[] moveAudioClips;
    public AudioClip[] biteAudioClips;

    public float MINPlayDelay => _minPlayDelay;
    public float MAXPlayDelay => _maxPlayDelay;
    public float MoveSoundProbability => _moveSoundProbability;
}