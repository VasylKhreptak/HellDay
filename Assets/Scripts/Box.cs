using UnityEngine;

public abstract class Box : MonoBehaviour
{ 
    protected abstract int Durability { get; set; }
    protected ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }
    
    protected abstract bool IsBroken();
    protected abstract void TakeDamage(int damage);
    protected abstract void SpawnDestroyParticle();
}