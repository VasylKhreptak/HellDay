using System;
using UnityEngine;

public class OnBulletHitEvent : MonoBehaviour
{
    [Header("Data")] 
    [SerializeField] private OnBulletHitEventData _data;
    
    public Action<Collision2D> onHit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.bulletLayerMask.ContainsLayer(other.gameObject.layer))
        {
            onHit?.Invoke(other);
        }
    }
}
