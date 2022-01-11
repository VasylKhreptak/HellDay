using UnityEngine;

[CreateAssetMenu(fileName = "UI_RandomAlphaData", menuName = "ScriptableObjects/UI_RandomAlphaData")]
public class UI_RandomAlphaData : ScriptableObject
{
    [Header("Alpha preferences")]
    [SerializeField] [Range(0f, 1f)] private float _minAlpha;
    [SerializeField] [Range(0f, 1f)] private float _maxAlpha;

    public float MinAlpha => _minAlpha;
    public float MaxAlpha => _maxAlpha;
}