using UnityEngine;
using  System.Linq;

[System.Serializable]
public class KillableTarget : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private IKillable _Killable;

    public Transform Transform => _transform;
    public IKillable Killable => _Killable;
    
    private void Awake()
    {
        _Killable = GetComponent<IKillable>();
    }
}
