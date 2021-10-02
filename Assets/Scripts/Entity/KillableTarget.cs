using UnityEngine;

[System.Serializable]
public class KillableTarget : MonoBehaviour
{
    public Transform _transform;
    public IKillable _Killable;
    
    private void Awake()
    {
        _Killable = GetComponent<IKillable>();
    }
}
