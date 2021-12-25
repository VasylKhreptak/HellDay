using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ParticleMovement : MonoBehaviour, IPooledObject
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Movement Data")]
    [SerializeField] private ParticleMovementData _movementData;


    public void OnEnable()
    {
        StartMovement();
    }

    private void StartMovement()
    {
        _rigidbody2D.velocity = new Vector2(Random.Range(_movementData.MINHorizontalVelocity,
            _movementData.MAXHorizontalVelocity), Random.Range(_movementData.MINVerticalVelocity,
            _movementData.MAXVerticalVelocity));

        _rigidbody2D.AddTorque(_movementData.Torque);
    }
}
