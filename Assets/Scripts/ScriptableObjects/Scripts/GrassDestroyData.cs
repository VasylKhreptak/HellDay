using UnityEngine;

[CreateAssetMenu(fileName = "GrassDestroyData", menuName = "ScriptableObjects/GrassDestroyData")]
public class GrassDestroyData : ScriptableObject
{
    [System.Serializable]
    public class GrassState
    {
        [Range(0f, 100f)] public float healthPercentage;
        public Sprite sprite;
        public Material shaderMaterial;
    }

    [Header("Grass Damage effect")]
    public Pools damageEffect;

    [Header("Preferences")]
    public GrassState[] grassStates;

    [Header("Data")]
    public AudioClip[] _stateChangeAudioClips;
}