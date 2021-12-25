using UnityEngine;

[CreateAssetMenu(fileName = "RandomDestroyParticleData", menuName = "ScriptableObjects/RandomDestroyParticleData")]
public class RandomDestroyParticleData : ScriptableObject
{
    [System.Serializable]
    public class DestroyParticle
    {
        public Pools particle;
        [Range(0f, 100f)] public float probability;
    }

    [Header("Particles")]
    public DestroyParticle[] destroyParticles;
}
