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

    private Tween _waitTween;

    private readonly int LegKick = Animator.StringToHash("LegKick");

    public void PlayLegKickAnimation()
    {
        _animator.SetTrigger(LegKick);
        this.DOWait(0.1f).OnComplete(() => { _animator.ResetTrigger(LegKick); });

        _isPlaying = true;
        _waitTween.Kill();
        _waitTween = this.DOWait(_legKickDuration).OnComplete(() => { _isPlaying = false; });

        onPlayed?.Invoke();
    }
}