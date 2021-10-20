using DG.Tweening;
using UnityEngine;

public class BulletMuff : MonoBehaviour, IPooledObject
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Preferences")]
    [SerializeField] private float _verticalVelocity = 1f;
    [SerializeField] private float _maxHorizontalVelocity = 2f;
    [SerializeField] private float _minHorizontalVelocity = 1f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private float _torque = 1f;


    public void OnEnable()
    {
        SetMovement();
        
        if (gameObject.activeSelf)
        {
            _transform.DOWait(_lifeTime, () => { gameObject.SetActive(false); });
        }
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity =
            new Vector2(
                Random.Range(-_maxHorizontalVelocity, -_minHorizontalVelocity) * PlayerMovement.MovementDirection,
                _verticalVelocity);

        _rigidbody2D.AddTorque(Random.Range(-_torque, _torque));
    }
}