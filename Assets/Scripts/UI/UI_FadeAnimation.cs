 using System;
 using System.Collections;
using System.Collections.Generic;
 using DG.Tweening;
 using UnityEngine;

public class UI_FadeAnimation : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] [Range(0f, 1f)] protected float _startAlpha = 0f;
    [SerializeField] [Range(0f, 1f)] protected float _targetAlpha = 1f;
    [SerializeField] protected float _fadeDuration = 0.7f;
    [SerializeField] protected AnimationCurve _fadeCurve;

    protected Tween _fadeTween;

    public virtual void Animate(bool show)
    {
        throw new NotImplementedException();
    }

    protected void OnDisable()
    {
       _fadeTween.Kill(); 
    }
}
