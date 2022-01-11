using UnityEngine;

public class OnDamagePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Data")]
    [SerializeField] private OnDamagePopupData _data;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        _damageableObject.onTakeDamage += SpawnDamagePopup;
    }

    private void OnDisable()
    {
        _damageableObject.onTakeDamage -= SpawnDamagePopup;
    }

    public void SpawnDamagePopup(float damage)
    {
        if (damage < 1) return;

        var obj = _objectPooler.GetFromPool(_data.damagePopupPool, _transform.position,
            Quaternion.identity);

        if (obj.TryGetComponent(out DamagePopup damagePopup))
            damagePopup.Init(((int)damage).ToString(), GetPopupColor(damage));
    }

    private Color GetPopupColor(float damage)
    {
        var clampedDamage = Mathf.Clamp(damage, _data.MINDamageColorValue, _data.MAXDamageColorValue);

        var delta01 = (clampedDamage - _data.MINDamageColorValue) /
                      (_data.MAXDamageColorValue - _data.MINDamageColorValue);

        return _data.colorGradient.Evaluate(delta01);
    }
}