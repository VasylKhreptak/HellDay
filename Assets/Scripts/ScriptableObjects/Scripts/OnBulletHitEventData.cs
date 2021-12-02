using UnityEngine;

[CreateAssetMenu(fileName = "OnBulletHitEventData", menuName = "ScriptableObjects/OnBulletHitEventData")]
public class OnBulletHitEventData : ScriptableObject
{
    [Header("Bullet LayerMask")] 
    public LayerMask bulletLayerMask;
}
