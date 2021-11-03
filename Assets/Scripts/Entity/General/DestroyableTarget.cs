using UnityEngine;

[System.Serializable]
public class KillableTarget : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private IDestroyable _destroyable;

    public Transform Transform => _transform;
    public IDestroyable Destroyable => _destroyable;
    
    private void Awake()
    {
        _destroyable = GetComponent<IDestroyable>();
    }
}
