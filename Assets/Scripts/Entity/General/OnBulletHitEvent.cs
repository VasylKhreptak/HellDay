using System;
using UnityEngine;

public class OnBulletHitEvent : MonoBehaviour
{
    [Header("Data")] 
    [SerializeField] private OnBulletHitEventData _data;
    
    public Action<Collision2D> onBulletHit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.bulletLayerMask.ContainsLayer(other.gameObject.layer))
        {
            onBulletHit.Invoke(other);
        }
    }
}
