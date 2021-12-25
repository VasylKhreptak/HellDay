using System.Collections;
using UnityEngine;

public class FuelBarrel : ExplosiveObjectCore
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Smoke Preferences")]
    [SerializeField] private ParticleSystem _smoke;

    [Header("Fuel Barrel Data")]
    [SerializeField] private FuelBarrelData _fuelBarrelData;

    private GameObject _smokeObj;
    private bool _isSmokeSpawned;
    private float _percentagedHealth;

    private void Start()
    {
        _percentagedHealth = _damageableObject.Health * (_fuelBarrelData.HealthPercentage / 100f);
    }

    private void OnEnable()
    {
        _damageableObject.onTakeDamage += ControlSmokeAppearance;
    }

    public void ControlSmokeAppearance(float damage)
    {
        if (_damageableObject.Health < _percentagedHealth) StartCoroutine(SmokeRoutine());
    }

    private IEnumerator SmokeRoutine()
    {
        _smoke.Play();

        yield return new WaitForSeconds(_fuelBarrelData.ExplodeDelay);

        ExplodeActions();
    }

    public void OnDisable()
    {
        _damageableObject.onTakeDamage -= ControlSmokeAppearance;

        if (gameObject.scene.isLoaded == false) return;

        ExplodeActions();
    }

    private void DisableSmoke()
    {
        if (_smokeObj == null) return;

        _smokeObj.SetActive(false);
    }

    private void ExplodeActions()
    {
        DisableSmoke();

        Explode();

        Destroy(gameObject);
    }
}
