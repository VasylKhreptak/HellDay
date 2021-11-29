using UnityEngine;

[CreateAssetMenu(fileName = "ObjectOnBulletHitData", menuName = "ScriptableObjects/ObjectOnBulletHitData")]
public class ObjectOnBulletHitData : ScriptableObject
{
    public LayerMask bulletLayerMask;
}
