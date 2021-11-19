using System;
using UnityEngine;

public class BonusItemCore : MonoBehaviour
{
    [Header("Player LayerMask")] 
    [SerializeField] protected LayerMask _playerLayerMask;

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (_playerLayerMask.ContainsLayer(other.gameObject.layer))
        {
            OnCollisionWithPlayer(other);
        }
    }

    protected virtual void OnCollisionWithPlayer(Collision2D player)
    {
        throw new NotImplementedException();
    }
}