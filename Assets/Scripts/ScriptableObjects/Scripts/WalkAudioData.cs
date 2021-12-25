using UnityEngine;

[CreateAssetMenu(fileName = "WalkAudioData", menuName = "ScriptableObjects/WalkAudioData")]
public class WalkAudioData : ScriptableObject
{
    [System.Serializable]
    public class StepAudio
    {
        public SurfaceTypes surfaceTypes;
        public AudioClip[] audioClips;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("Audios")]
    public StepAudio[] stepAudios;
    public AudioClip[] defaultStepAudios;
}
