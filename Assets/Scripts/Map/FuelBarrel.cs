using System.Collections;
using UnityEngine;

public class FuelBarrel: ExplosiveObjectCore
{
    [Header("Smoke Preferences")]
    [SerializeField] private Transform _smokeSpawnPlace;
    [SerializeField] private float _explodeDelay = 2f;
    [SerializeField] private int _maxTakeDamageNumber = 4;
    [SerializeField] private Pools _smoke;

    private GameObject _smokeObj;
    private bool _isSmokeSpawned;
    private ObjectPooler _objectPooler;
    private int _currentTakeDamageNumber;
    
    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void OnTakeDamage()
    {
        if (++_currentTakeDamageNumber == _maxTakeDamageNumber)
        {
            StartCoroutine(SmokeRoutine());
        }
    }

    private IEnumerator SmokeRoutine()
    {
        _smokeObj = _objectPooler.GetFromPool(_smoke, _smokeSpawnPlace.position, Quaternion.identity);
        _smokeObj.transform.parent = _transform;

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
    }

    private void ExplodeActions()
    {
        DisableSmoke();
        
        _objectPooler.GetFromPool(_explosionEffect, _transform.position, Quaternion.identity);

        Explode();

        Destroy(gameObject);
    }
}