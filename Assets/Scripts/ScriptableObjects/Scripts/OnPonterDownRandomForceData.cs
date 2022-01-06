using UnityEngine;

[CreateAssetMenu(fileName = "OnPonterDownRandomForceData", menuName = "ScriptableObjects/OnPonterDownRandomForceData")]
public class OnPonterDownRandomForceData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private float _MINForce;
    [SerializeField] private float _MAXForce;
    [SerializeField] private float _MINTorque;
    [SerializeField] private float _MAXTorque;

    public ForceMode2D forceMode2D = ForceMode2D.Impulse;

    public float MinForce => _MINForce;
    public float MaxForce => _MAXForce;
    public float MinTorque => _MINTorque;
    public float MaxTorque => _MAXTorque;

}
