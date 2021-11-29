using UnityEngine;

[CreateAssetMenu(fileName = "OnBulletHitParticleData", menuName = "ScriptableObjects/OnBulletHitParticleData")]
public class OnBulletHitParticleData : ScriptableObject
{
    [Header("Preferences")]
    public LayerMask bulletLayerMask;
    public Pools particle;
}
