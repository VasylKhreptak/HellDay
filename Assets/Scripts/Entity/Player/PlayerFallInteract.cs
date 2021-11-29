using UnityEngine;

public class PlayerFallInteract : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private DamageableTarget _playerDamageableTarget;
    
    [Header("Preferences")] 
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _damageApmlifier = 0.5f;
    [SerializeField] private float _minDamageImpulse;

    [Header("Player Audio")] 
    [SerializeField] private PlayerAudio _playerAudio;

    private void OnCollisionEnter2D(Collision2D other)
    {
        float impulse = other.GetImpulse();
        
        if (_groundLayerMask.ContainsLayer(other.gameObject.layer) && impulse > _minDamageImpulse)
        {
            _playerDamageableTarget.Damageable.TakeDamage(impulse * _damageApmlifier);
            
            _playerAudio.PlayBoneCrackSound();
        }
    }
}
