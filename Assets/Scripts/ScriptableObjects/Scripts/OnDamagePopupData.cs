using UnityEngine;

[CreateAssetMenu(fileName = "OnDamagePopupData", menuName = "ScriptableObjects/OnDamagePopupData")]
public class OnDamagePopupData : ScriptableObject
{
    [Header("Preferences")]
    public Gradient colorGradient;
    public Pools damagePopupPool;

    [SerializeField] private float _minDamageColorValue;
    [SerializeField] private float _maxDamageColorValue;

    public float MINDamageColorValue => _minDamageColorValue;
    public float MAXDamageColorValue => _maxDamageColorValue;
}
