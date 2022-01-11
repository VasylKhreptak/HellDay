using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData")]
public class BulletData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _lifeTime = 0.5f;

    public float Speed => _speed;
    public float LifeTime => _lifeTime;
}