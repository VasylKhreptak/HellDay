using UnityEngine;

public class PlayerFallControl : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private KillableTarget _playerKillableTarget;
    [SerializeField] private Transform _transform;
    
    [Header("Preferences")] 
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _landingDamage = 40f;
    [SerializeField] private float _minDamageImpulse;

    [Header("Bone Crack Audio Clips")]
    [SerializeField] private AudioClip[] _boneAudioClips;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_groundLayerMask.ContainsLayer(other.gameObject.layer) &&
            other.GetImpulse() > _minDamageImpulse)
        {
            _playerKillableTarget.Killable.TakeDamage(_landingDamage);
            
            RandomAudio.Play(_transform.position, _boneAudioClips);
        }
    }
}
