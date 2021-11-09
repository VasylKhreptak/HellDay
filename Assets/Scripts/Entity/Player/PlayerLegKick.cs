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
    [SerializeField] private float _kickDelay;
    [SerializeField] private float _delayBeforeKick = 0.2f;
    [SerializeField] private float _entityDamage = 30f;
 
    [Header("Player Animation")] 
    [SerializeField] private PlayerAnimation _playerAnimation;

    private bool _canKick = true;
    
    public void KickActions()
    {
        if (_playerObj.activeSelf == false || _canKick == false) return;
        
        _playerAnimation.LegKick();

        this.DOWait(_delayBeforeKick).OnComplete(() =>
        {
            Kick();
        });
        
        ControlKickSpeed();
    }

    private void Kick()
    {
        GameObject atackedObj = GetAttackedObject();
            
        if(atackedObj == null) return;

        if (atackedObj.TryGetComponent(out Rigidbody2D rb))
        {
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = new Vector2(_kickVelocity * PlayerMovement.MovementDirection,
                _upwardsVelocity);
            }
        }
        if (atackedObj.CompareTag("Zombie") && atackedObj.TryGetComponent(out KillableTarget target))
        {
            target.Killable.TakeDamage(_entityDamage);
        }
    }

    private void ControlKickSpeed()
    {
        _canKick = false;
        this.DOWait(_kickDelay).OnComplete(() => { _canKick = true; });
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

    private void OnDestroy()
    {
        this.DOKill();
    }

    private void OnDrawGizmosSelected()
    {
        if (_startKickTransform == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_startKickTransform.position, _startKickTransform.position + new Vector3(_kickLength, 0, 0));
    }
}
