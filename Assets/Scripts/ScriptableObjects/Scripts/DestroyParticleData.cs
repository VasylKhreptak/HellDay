using UnityEngine;

[CreateAssetMenu(fileName = "DestroyParticleData", menuName = "ScriptableObjects/DestroyParticleData")]
public class DestroyParticleData : ScriptableObject
{
    public Pools[] destroyParticles;
}
