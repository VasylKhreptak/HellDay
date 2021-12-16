using DG.Tweening;
using UnityEngine;

public class PlayerLegKick : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _startKickTransform;
    [SerializeField] private GameObject _playerObj;

    [Header("Preferences")] 
    [SerializeField] private float _kickVelocity;
    [SerializeField] private float _upwardsVelocity;
    [SerializeField] private float _kickLength;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ForceMode2D _forceMode2D;

    [Header("Damage")] 
    [SerializeField] private float _minKickDamage = 20f;
    [SerializeField] private float _maxKickDamage = 30f;
    
    [Header("KickTorque")] 
    [SerializeField] private float _minKickTorque;
    [SerializeField] private float _maxKickTorque;

    private float DamageValue => Random.Range(_minKickDamage, _maxKickDamage);

    public void Kick()
    {
        if (CanKick() == false) return;
        
        GameObject atackedObj = GetAttackedObject();
        
        if(atackedObj == null) return;

        DiscardObject(atackedObj);

        DamageObject(atackedObj);
    }

    private bool CanKick()
    {
        return _playerObj.activeSelf;
    }

    private void DiscardObject(GameObject atackedObj)
    {
        if (atackedObj.TryGetComponent(out Rigidbody2D rb))
        {
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = new Vector2(_kickVelocity * PlayerMovement.Direction,
                    _upwardsVelocity);
                
                rb.AddTorque(Random.Range(-_minKickTorque, _maxKickTorque), _forceMode2D);
            }
        }
    }

    private void DamageObject(GameObject atackedObj)
    {
        if (atackedObj.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(DamageValue);
        }
    }

    private GameObject GetAttackedObject()
    {
        Vector2 rayDir = new Vector2(PlayerMovement.Direction, 0);
        RaycastHit2D hit;
        
        hit = Physics2D.Raycast(_startKickTransform.position, rayDir, _kickLength, _layerMask);

        if (hit.collider == null)
        {
            return null;
        }
        
        return hit.collider.gameObject;
    }

    private void OnDrawGizmosSelected()
    {
        if (_startKickTransform == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_startKickTransform.position, _startKickTransform.position + new Vector3(_kickLength, 0, 0));
    }
}
