using UnityEngine;

[CreateAssetMenu(fileName = "OnBulletHitImpulseData", 
    menuName = "ScriptableObjects/OnBulletHitImpulseData")]
public class OnBulletHitImpulseData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _minImpulse;
    [SerializeField] private float _maxImpulse;

    public float Impulse => Random.Range(_minImpulse, _maxImpulse);
}
