using UnityEngine;

[CreateAssetMenu(fileName = "GreenZombieAtackData", menuName = "ScriptableObjects/GreenZombieAtackData")]
public class GreenZombieAtackData : ZombieAtackCoreData
{
    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius = 7f;

    public LayerMask environmentLayerMask;

    public float ExplosionRadius => _explosionRadius;
}
