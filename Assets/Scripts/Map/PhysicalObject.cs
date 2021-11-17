using System;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalObject : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] protected float _maxDurability = 7f;
    protected float _durability;
    
    [Header("Take Damage Evenet")] 
    public UnityEvent OnTakeDamage;
    
    protected virtual void Start()
    {
        SetMaxDurability(_maxDurability);
    }
    
    protected virtual void SetMaxDurability(float maxDurability)
    {
        _durability = _maxDurability;
    }
    
    public virtual void TakeDamage(float damage)
    {
        _durability -= damage;

        OnTakeDamage.Invoke();
        
        if (IsBroken())
        {
            DestroyActions();
        }
    }

    public virtual void DestroyActions()
    {
        throw new NotImplementedException();
    }

    protected bool IsBroken() => _durability <= 0;
}
