using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStripes : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;

    [Header("Preferences")]
    [SerializeField] private int _framerate  = 10;

    private readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        StartCoroutine(ConfigurableUpdate(_framerate));
    }

    private IEnumerator ConfigurableUpdate(int framerate)
    {
        float delay = 1 / framerate;

        while (true)
        {
            MoveStripes();

            yield return new WaitForSeconds(delay);
        }
    }

    private void MoveStripes()
    {
        _animator.SetBool(IsMoving, _rigidbody2D.velocity != Vector2.zero);
    }
}
