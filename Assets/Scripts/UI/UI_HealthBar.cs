using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    [SerializeField] private float _time = 1;
    [SerializeField] private AnimationCurve _animationCurve;
    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SET_MAX_HEALTH_BAR, SetMaxHealthBar);
        Messenger<float>.AddListener(GameEvent.SET_HEALTH_BAR, SetHealthBar);
    }

    private void OnDestroy()
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
        DOTween.Kill(gameObject);
        
        DOTween.To(() => _slider.value, x => _slider.value = x, health, _time).
            SetEase(_animationCurve);
        _fill.DOColor(_gradient.Evaluate(health / _slider.maxValue), _time).
            SetEase(_animationCurve);
    }
}
