using UnityEngine;

[CreateAssetMenu(fileName = "BulletMuffAudioData", menuName = "ScriptableObjects/BulletMuffAudioData")]
public class BulletMuffAudioData : ScriptableObject
{
    [System.Serializable]
    public class MuffAudio
    {
        public SurfaceTypes surfaceType;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [System.Serializable]
    public class PlaySoundProbability
    {
        public Weapons weaponType;
        [Range(0f, 100f)] public float probability;
    }

    public AudioClip[] audioClips;
    public MuffAudio[] muffAudios;
    public PlaySoundProbability[] playSoundProbabilities;
    [SerializeField] private float _tileDetectLength = 0.3f;

    public float TileDetectLength => _tileDetectLength;
}