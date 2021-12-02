using System;
using UnityEngine;

public class OnPhysicalHit : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Preferences")] 
    [SerializeField] private OnPhysicalHitData _data;

    public Action onPhysicalHit;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.layerMask.ContainsLayer(other.gameObject.layer) == false) return;
        
        float impulse = other.GetImpulse();
    
        if (impulse > _data.MINDamageImpulse)
        {
            _damageableObject.TakeDamage(impulse * _data.DamageAmplifier);
            
            onPhysicalHit?.Invoke();
        }
    }
}
