using UnityEngine;

[CreateAssetMenu(fileName = "GrayZombieAtackData", menuName = "ScriptableObjects/GrayZombieAtackData")]
public class GrayZombieAtackData : CommonZombieAtackData
{
    [Header("Atack Preferences")]
    [SerializeField] private float _minJumpRadius = 1.92f;
    [SerializeField] private float _maxJumpRadius = 6.25f;
    [SerializeField] private float _jumpSpeed = 18.3f;

    public float MINJumpRadius => _minJumpRadius;
    public float MAXJumpRadius => _maxJumpRadius;
    public float JumpSpeed => _jumpSpeed;
}