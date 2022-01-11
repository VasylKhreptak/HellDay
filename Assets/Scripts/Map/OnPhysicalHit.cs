using System;
using UnityEngine;

public class OnPhysicalHit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Preferences")]
    [SerializeField] private OnPhysicalHitData _data;

    public Action onHit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.layerMask.ContainsLayer(other.gameObject.layer) == false) return;

        var impulse = other.GetImpulse();

        if (impulse > _data.MINDamageImpulse)
        {
            _damageableObject.TakeDamage(impulse * _data.DamageAmplifier);

            onHit?.Invoke();
        }
    }
}