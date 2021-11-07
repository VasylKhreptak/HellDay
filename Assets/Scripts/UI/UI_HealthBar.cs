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

    private Tween _tween;
    
    private void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.SET_MAX_HEALTH_BAR, SetMaxHealthBar);
        Messenger<float>.AddListener(GameEvent.SET_HEALTH_BAR, SetHealthBar);
    }

    private void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.SET_MAX_HEALTH_BAR, SetMaxHealthBar);
        Messenger<float>.RemoveListener(GameEvent.SET_HEALTH_BAR, SetHealthBar);
    }

    public void SetMaxHealthBar(float health)
    {
        _slider.maxValue = health;
        
        SetHealthBar(health);
    }
    
    public void SetHealthBar(float health)
    {
        _tween.Kill();
        
        _tween = DOTween.To(() => _slider.value, x => _slider.value = x, health, _moveTime).
            SetEase(_animationCurve);
        _fill.DOColor(_gradient.Evaluate(health / _slider.maxValue), _moveTime).
            SetEase(_animationCurve);
    }
}
