using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    [SerializeField] private float _moveTime = 1;
    [SerializeField] private AnimationCurve _animationCurve;

    private Tween _sliderTween;

    private void OnEnable()
    {
        Player.onSetMaxHealthBar += SetMaxHealthBar;
        Player.onSetHealthBar += SetHealthBar;
    }

    private void OnDisable()
    {
        Player.onSetMaxHealthBar -= SetMaxHealthBar;
        Player.onSetHealthBar -= SetHealthBar;
    }

    public void SetMaxHealthBar(float health)
    {
        _slider.maxValue = health;

        SetHealthBar(health);
    }

    public void SetHealthBar(float health)
    {
        _sliderTween.Kill();

        _sliderTween = DOTween.To(() => _slider.value, x => _slider.value = x, health, _moveTime).SetEase(_animationCurve);
        _fill.DOColor(_gradient.Evaluate(health / _slider.maxValue), _moveTime).SetEase(_animationCurve);
    }
}
