using UnityEngine;

public class PlayerFallInteract : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private DamageableTarget _playerDamageableTarget;
    
    [Header("Preferences")] 
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _landingDamage = 40f;
    [SerializeField] private float _minDamageImpulse;

    [Header("Player Audio")] 
    [SerializeField] private PlayerAudio _playerAudio;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_groundLayerMask.ContainsLayer(other.gameObject.layer) &&
            other.GetImpulse() > _minDamageImpulse)
        {
            _playerDamageableTarget.Damageable.TakeDamage(_landingDamage);
            
            _playerAudio.PlayBoneCrackSound();
        }
    }
}
