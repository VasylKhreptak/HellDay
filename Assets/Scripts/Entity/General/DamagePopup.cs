using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _TMP;
    [SerializeField] private RectTransform _rectTransform;

    [Header("Data")]
    [SerializeField] private DamagePopupData _data;

    private Tween _waitTween, _fadeTween, _scaleTween;

    private void Start()
    {
        _data.previousScale = _rectTransform.localScale;
    }

    public void Init(string damage, Color color)
    {
        _TMP.text = damage;
        _TMP.color = color;
    }

    private void OnEnable()
    {
        KillTweens();

        _rectTransform.localScale = _data.previousScale;

        _TMP.color = _TMP.color.WithAlpha(0f);

        _waitTween = this.DOWait(_data.Lifetime).OnComplete(() => { gameObject.SetActive(false); });
        _fadeTween = _TMP.DOFade(0, _data.Lifetime).SetEase(_data._alphaCurve);
        _scaleTween = _rectTransform.DOScale(Vector3.zero, _data.Lifetime).SetEase(_data._scaleCurve);
    }

    private void OnDisable()
    {
        KillTweens();
    }

    private void KillTweens()
    {
        _waitTween.Kill();
        _fadeTween.Kill();
        _scaleTween.Kill();
    }
}