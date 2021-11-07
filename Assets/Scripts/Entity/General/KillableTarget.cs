using UnityEngine;

[System.Serializable]
public class KillableTarget : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private IKillable _killable;

    public Transform Transform => _transform;
    public IKillable Killable => _killable;
    
    private void Awake()
    {
        if (TryGetComponent(out IKillable killable))
        {
            _killable = killable;
        }
    }
}
