using UnityEngine;

[CreateAssetMenu(fileName = "OnBulletHitParticleData", menuName = "ScriptableObjects/OnBulletHitParticleData")]
public class OnBulletHitParticleData : ScriptableObject
{
    [Header("Preferences")]
    public Pools particle;
}
