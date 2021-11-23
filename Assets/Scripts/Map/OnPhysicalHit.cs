using UnityEngine;

public class OnPhysicalHit : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Preferences")] 
    [SerializeField, Min(0f)] private float _minDamageImpulse = 60f;
    [SerializeField, Min(0f)] private float _damageAmplifier = 0.2f;
    [SerializeField] private LayerMask _layerMask;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer) == false) return;
        
        float impulse = other.GetImpulse();
    
        if (impulse > _minDamageImpulse)
        {
            _damageableObject.TakeDamage(impulse * _damageAmplifier);
        }
    }
}
