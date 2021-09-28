using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected float _maxDurability = 7f;
    protected float _durability;
    
    protected virtual void Start()
    {
        SetMaxDurability(_maxDurability);
    }
    
    protected virtual void SetMaxDurability(float maxDurability)
    {
        _durability = _maxDurability;
    }
    
    protected virtual void TakeDamage(float damage)
    {
        _durability -= damage;

        if (IsBroken())
        {
            DestroyActions();
            
            Destroy(gameObject);
        }
    }

    protected virtual void DestroyActions()
    {
        
    }
    
    protected bool IsBroken()
    {
        return _durability <= 0;
    }
}
