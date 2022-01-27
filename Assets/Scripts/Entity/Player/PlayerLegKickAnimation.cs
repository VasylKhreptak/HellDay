using System;
using DG.Tweening;
using UnityEngine;

public class PlayerLegKickAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    public static Action onPlayed;
    [SerializeField] private float _legKickDuration = 0.6f;

    private static bool _isPlaying;
    public static bool IsPlaying => _isPlaying;

    private Tween _waitTween1, _waitTween2;

    private readonly int LegKick = Animator.StringToHash("LegKick");

    public void PlayLegKickAnimation()
    {
        _animator.SetTrigger(LegKick);
        _waitTween2 = this.DOWait(0.1f).OnComplete(() => { _animator.ResetTrigger(LegKick); });

        _isPlaying = true;
        _waitTween1.Kill();
        _waitTween1 = this.DOWait(_legKickDuration).OnComplete(() => { _isPlaying = false; });

        onPlayed?.Invoke();
    }

    private void OnDisable()
    {
        _waitTween1.Kill();
        _waitTween2.Kill();
    }
}