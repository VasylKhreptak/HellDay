using UnityEngine;

[System.Serializable]
public class DamageableTarget : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private IDamageable _damageable;

    public Transform Transform => _transform;
    public IDamageable Damageable => _damageable;

    private void Awake()
    {
        if (TryGetComponent(out IDamageable damageable)) _damageable = damageable;
    }
}
