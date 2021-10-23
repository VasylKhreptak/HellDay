
using TMPro;
using UnityEngine;

public class FuelBarrel : DestroyableItem
{
    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius;
    //[SerializeField] private float _cameraImpactRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _upwardsModifier;
    [SerializeField] private ForceMode2D _forceMode2D;
    [SerializeField] private AnimationCurve _forceCurve;

    [Header("Audio")] 
    [SerializeField] private AudioClip _explosionAudioClip;
    [SerializeField, Range(0f, 1f)] private float _soundVolume = 1f;
    
    protected override void DestroyActions()
    {
        AudioSource.PlayClipAtPoint(_explosionAudioClip, _transform.position, _soundVolume);
        
        Explode();
        
        Destroy(gameObject);

        _objectPooler.GetFromPool(Pools.BarrelExplosion, _transform.position, Quaternion.identity);
    }

    private void Explode()
    {
        Messenger.Broadcast(GameEvent.SHAKE_CAMERA);
        
        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(_transform.position,
            _explosionRadius, _layerMask);
        
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody2D rigidbody2D = overlappedColliders[i].attachedRigidbody;

            if (rigidbody2D != null)
            {
                rigidbody2D.AddExplosionForce(_explosionForce, _transform.position, _explosionRadius,
                    _forceCurve, _upwardsModifier, _forceMode2D);
            }
        }
    }
    
#if  UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_transform == null)
        {
            return;
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
#endif
}