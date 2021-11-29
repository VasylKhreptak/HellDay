using System.Collections;
using UnityEngine;

public class FuelBarrel: ExplosiveObjectCore
{
    [Header("Smoke Preferences")]
    [SerializeField] private Transform _smokeSpawnPlace;

    [Header("Fuel Barrel Data")] 
    [SerializeField] private FuelBarrelData _fuelBarrelData;

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
        if (++_currentTakeDamageNumber == _fuelBarrelData.MAXTakeDamageNumber)
        {
            StartCoroutine(SmokeRoutine());
        }
    }

    private IEnumerator SmokeRoutine()
    {
        _smokeObj = _objectPooler.GetFromPool(_fuelBarrelData.Smoke, _smokeSpawnPlace.position, Quaternion.identity);
        _smokeObj.transform.parent = _transform;

        yield return new WaitForSeconds(_fuelBarrelData.ExplodeDelay);
        
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

        Explode();

        Destroy(gameObject);
    }
}