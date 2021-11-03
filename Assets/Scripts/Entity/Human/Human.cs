using UnityEngine;

public class Human : Entity, IDestroyable
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")] 
    [SerializeField] private Pools _deathParticle;
    
    private ObjectPooler _objectPooler;

    protected override void Start()
    {
        SetMaxHealth(_maxHealth);
        
        _objectPooler = ObjectPooler.Instance;
    }

    protected override void DeathActions()
    {
        SpawnDeathParticle();
            
        Destroy(gameObject);
    }

    public void SpawnDeathParticle()
    {
        _objectPooler.GetFromPool(_deathParticle, _transform.position, Quaternion.identity);
    }
    
    public void SaveHuman()
    {
        Debug.Log("Human Saved!");
    }
}
