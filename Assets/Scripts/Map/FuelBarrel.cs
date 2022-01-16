using System;
using System.Collections;
using UnityEngine;

public class FuelBarrel : ExplosiveObjectCore
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Fuel Barrel Data")]
    [SerializeField] private FuelBarrelData _fuelBarrelData;

    [Header("Preferences")]
    [SerializeField] private GameObject _smokeObj;

    private bool _isSmokeSpawned;
    private float _percentagedHealth;

    public static Action onExplode;

    private void Awake()
    {
        _smokeObj.SetActive(false);
    }

    private void Start()
    {
        _percentagedHealth = _damageableObject.Health * (_fuelBarrelData.HealthPercentage / 100f);
    }

    private void OnEnable()
    {
        _damageableObject.onTakeDamage += ControlSmokeAppearance;
    }

    public void OnDisable()
    {
        _damageableObject.onTakeDamage -= ControlSmokeAppearance;

        if (gameObject.scene.isLoaded == false)
            return;

        ExplodeActions();
    }

    private void ControlSmokeAppearance(float damage)
    {
        if (_damageableObject.Health < _percentagedHealth &&
            _isSmokeSpawned == false)
        {
            _isSmokeSpawned = true;
            
            StartCoroutine(SmokeRoutine());
        }
    }

    private IEnumerator SmokeRoutine()
    {
        _smokeObj.SetActive(true);

        yield return new WaitForSeconds(_fuelBarrelData.ExplodeDelay);

        ExplodeActions();
    }

    private void DisableSmoke()
    {
        if (_smokeObj == null)
            return;

        _smokeObj.SetActive(false);
    }

    private void ExplodeActions()
    {
        onExplode?.Invoke();

        DisableSmoke();

        Explode();

        Destroy(gameObject);
    }
}