using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FuelBarrel : DestroyableObject
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Preferences")] 
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _upwardsModifier;
    [SerializeField] private ForceMode2D _forceMode2D;
    [SerializeField] private AnimationCurve _forceCurve;
    [SerializeField] private float _chainExplosionDelay = 0.5f;
    [SerializeField] private Pools _fuelBarrelExplosion = Pools.FuelBarrelExplosion;
    
    [Header("Entity Damage")] 
    [SerializeField] private float _maxEntityDamage = 100f;
    [SerializeField] private AnimationCurve _entityDamageCurve;

    [Header("Physical Object Damage")] 
    [SerializeField] private float _maxObjectDamage = 25f;
    [SerializeField] private AnimationCurve _objectDamageCurve;

    [Header("Smoke Preferences")]
    [SerializeField] private Transform _smokeSpawnPlace;
    [SerializeField] private float _explodeDelay = 2f;
    [SerializeField, Range(0, 0.99f)] private float _percentage;
    [SerializeField] private Pools _smoke;

    private GameObject _smokeObj;
    private Transform _previousSmokeParent;
    private float _percentagedMaxDurability;
    private bool _isSmokeSpawned;

    private void Awake()
    {
        _percentagedMaxDurability = _maxDurability * _percentage;
    }

    public override void TakeDamage(float damage)
    {
        if (_durability < _percentagedMaxDurability && _isSmokeSpawned == false)
        {
            StartCoroutine(SmokeRoutine());

            _isSmokeSpawned = true;
        }
        
        base.TakeDamage(damage);
    }

    private IEnumerator SmokeRoutine()
    {
        _smokeObj = _objectPooler.GetFromPool(_smoke, _smokeSpawnPlace.position, Quaternion.identity);
        _previousSmokeParent = _smokeObj.transform.parent;
        
        yield return new WaitForSeconds(_explodeDelay);
        
        ExplodeActions();
    }

    public void OnDisable()
    {
        if (gameObject.scene.isLoaded == false) return;
        
        ExplodeActions();
    }

    private void DisableSmoke()
    {
        if(_smokeObj == null) return;
        
        _smokeObj.SetActive(false);
        _smokeObj.transform.parent = _previousSmokeParent;
    }

    private void ExplodeActions()
    {
        _objectPooler.GetFromPool(_fuelBarrelExplosion, _transform.position, Quaternion.identity);

        DisableSmoke();
        
        Explode();

        Destroy(gameObject);
    }

    private  void Explode()
    {
        Messenger.Broadcast(GameEvents.PLAYED_LOUD_AUDIO_SOURCE);
        Messenger<Vector3>.Broadcast(GameEvents.SHAKE_CAMERA, _transform.position);

        Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(_transform.position,
            _explosionRadius, _layerMask);

        foreach (var coll in overlappedColliders)
        {
            InteractWithCollider(coll);
        }
    }

    private void InteractWithCollider(Collider2D collider2D)
    {
        SpreadOutObject(collider2D);

        if (collider2D.isActiveAndEnabled == false) return;

        if (collider2D.CompareTag("FuelBarrel"))
        {
            ExplodeChainedBarrel(collider2D);
        }
        else if (collider2D.TryGetComponent(out KillableTarget target))
        {
            target.Killable.TakeDamage(_entityDamageCurve.Evaluate(target.Transform.position, 
                _transform.position, _maxEntityDamage, _explosionRadius));
        }
        else if (collider2D.TryGetComponent(out DestroyableObject destroyableObject))
        {
            destroyableObject.TakeDamage( _objectDamageCurve.Evaluate(destroyableObject.Transform.position,
                _transform.position, _maxObjectDamage, _explosionRadius));
        }
    }

    private void SpreadOutObject(Collider2D collider2D)
    {
        Rigidbody2D rigidbody2D = collider2D.attachedRigidbody;

        if (rigidbody2D != null)
        {
            rigidbody2D.AddExplosionForce(_explosionForce, _transform.position, _explosionRadius,
                _forceCurve, _upwardsModifier, _forceMode2D);
        }
    }

    private void ExplodeChainedBarrel(Collider2D collider2D)
    {
        this.DOWait(_chainExplosionDelay).OnComplete(() =>
        {
            if (collider2D != null)
            {
                collider2D.gameObject.SetActive(false);
            }
        });
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _playerTransform == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _explosionRadius);
    }
}