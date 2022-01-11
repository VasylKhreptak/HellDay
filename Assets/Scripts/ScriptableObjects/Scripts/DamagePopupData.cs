using UnityEngine;

[CreateAssetMenu(fileName = "DamagePopupData", menuName = "ScriptableObjects/DamagePopupData")]
public class DamagePopupData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] [Min(0)] private float _lifetime;
    public AnimationCurve _scaleCurve;
    public AnimationCurve _alphaCurve;
    [HideInInspector] public Vector3 previousScale;

    public float Lifetime => _lifetime;
}