using UnityEngine;

[CreateAssetMenu(fileName = "TouchCounterEventData", menuName = "ScriptableObjects/TouchCounterEventData")]
public class TouchCounterEventData : ScriptableObject
{
    [Header("Preferences")]
    [SerializeField] private int _countEvent;

    public int CountEvent => _countEvent;
}
