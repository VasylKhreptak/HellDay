using System.Collections;
using UnityEngine;

public class BulletMuff : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _verticalVelocity = 1f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _maxHorizontalVelocity = 2f;
    [SerializeField] private float _minHorizontalVelocity = 1f;
    [SerializeField] private float _lifeTime = 2f;
    [SerializeField] private float _torque = 1f;


    public void OnEnable()
    {
        SetMovement();

        if (gameObject.activeSelf == true)
            StartCoroutine(DisableObject(_lifeTime));
    }

    private void SetMovement()
    {
        _rigidbody2D.velocity =
            new Vector2(
                Random.Range(-_maxHorizontalVelocity, -_minHorizontalVelocity) * PlayerMovement.movementDirection,
                _verticalVelocity);

        _rigidbody2D.AddTorque(Random.Range(-_torque, _torque));
    }

    private IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}