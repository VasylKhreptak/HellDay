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
    [SerializeField] private float _kickDamage = 30f;
    
    [Header("KickTorque")] 
    [SerializeField] private float _minKickTorque;
    [SerializeField] private float _maxKickTorque;

    private bool _canKick = true;

    public void Kick()
    {
        if (_playerObj.activeSelf == false || _canKick == false) return;
        
        GameObject atackedObj = GetAttackedObject();
        
        if(atackedObj == null) return;

        DiscardObject(atackedObj);

        DamageObject(atackedObj);
    }

    private void DiscardObject(GameObject atackedObj)
    {
        if (atackedObj.TryGetComponent(out Rigidbody2D rb))
        {
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = new Vector2(_kickVelocity * PlayerMovement.MovementDirection,
                    _upwardsVelocity);
                
                rb.AddTorque(Random.Range(-_minKickTorque, _maxKickTorque), _forceMode2D);
            }
        }
    }

    private void DamageObject(GameObject atackedObj)
    {
        if (atackedObj.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_kickDamage);
        }
    }

    private GameObject GetAttackedObject()
    {
        Vector2 rayDir = new Vector2(PlayerMovement.MovementDirection, 0);
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
