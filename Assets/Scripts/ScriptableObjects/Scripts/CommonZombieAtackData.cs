using UnityEngine;

[CreateAssetMenu(fileName = "CommonZombieAtackData", menuName = "ScriptableObjects/CommonZombieAtackData")]
public class CommonZombieAtackData : ZombieAtackCoreData
{
    [Header("Atack Preferences")] 
    [SerializeField] private float _biteRadius = 0.1f;

    public LayerMask entityLayerMask;
    
    public float BiteRadius => _biteRadius;
}