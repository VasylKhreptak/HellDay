using UnityEngine;

[CreateAssetMenu(fileName = "OnTouchCounterReachReloadSceneData", 
    menuName = "ScriptableObjects/OnTouchCounterReachReloadSceneData")]
public class OnTouchCounterReachReloadSceneData : ScriptableObject
{
    [Header("Prefrences")]
    [SerializeField] private float _reloadDelay;

    public float ReloadDelay => _reloadDelay;
}
