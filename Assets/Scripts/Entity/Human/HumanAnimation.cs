using System;
using UnityEngine;

public class HumanAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [Header("Preferences")]
    [SerializeField] private int _updateFrameRate = 10;

    private Coroutine _configurableUpdate;
    private void Awake()
    {
        ConfigurableUpdate.StartUpdate(this, ref _configurableUpdate, _updateFrameRate, () =>
        {
            _animator.SetFloat("Speed", Math.Abs(_rigidbody2D.velocity.x));
        });
    }
}
