using UnityEngine;

public class PlayerJumpAndLandAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private WalkAudio _walkAudio;

    private bool _playerJumped;

    private readonly int Jump = Animator.StringToHash("Jump");
    private readonly int Landed = Animator.StringToHash("Landed");

    private void OnEnable()
    {
        PlayerMovement.onJumped += OnPlayerJumped;
    }

    private void OnDisable()
    {
        PlayerMovement.onJumped -= OnPlayerJumped;
    }

    private void OnPlayerJumped()
    {
        _animator.ResetTrigger(Landed);
        _animator.SetTrigger(Jump);

        _playerJumped = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_playerJumped == false) return;

        _animator.ResetTrigger(Jump);
        _animator.SetTrigger(Landed);

        _walkAudio.PlayStepSound();

        _playerJumped = false;
    }
}