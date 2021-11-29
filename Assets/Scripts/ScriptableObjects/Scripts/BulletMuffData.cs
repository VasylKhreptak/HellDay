using UnityEngine;

[CreateAssetMenu(fileName = "BulletMuffData", menuName = "ScriptableObjects/BulletMuffData")]
public class BulletMuffData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _vertVelocity = 4f;
    [SerializeField] private float _maxHorVelocity = 2f;
    [SerializeField] private float _minHorVelocity = 1f;
    [SerializeField] private float _lifeTime = 6f;
    [SerializeField] private float _torque = 4f;

    public float VertVelocity => _vertVelocity;
    public float MAXHorVelocity => _maxHorVelocity;
    public float MINHorVelocity => _minHorVelocity;
    public float LifeTime => _lifeTime;
    public float Torque => _torque;
}
