using UnityEngine;

[CreateAssetMenu(fileName = "OnBulletHitAudioData", menuName = "ScriptableObjects/OnBulletHitAudioData")]
public class OnBulletHitAudioData : ScriptableObject
{
    [Header("Audio Clips")]
    public AudioClip[] audioClips;

    [Header("Preferences")]
    private float _minDelay;

    public float MINDelay => _minDelay;

    private void Awake()
    {
        var avarageLength = GetAvarageClipLength();

        _minDelay = avarageLength - avarageLength / 2;
    }

    private float GetAvarageClipLength()
    {
        var sum = 0f;

        foreach (var audio in audioClips) sum += audio.length;

        return sum / audioClips.Length;
    }
}
