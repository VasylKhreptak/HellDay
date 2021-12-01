using System;
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
    
    private void Awake()
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
        _rectTransform.localScale = _data.previousScale;
        
        this.DOWait(_data.Lifetime).OnComplete(() => { gameObject.SetActive(false);});
        _TMP.DOFade(0, _data.Lifetime).SetEase(_data._alphaCurve);
        _rectTransform.DOScale(Vector3.zero, _data.Lifetime).SetEase(_data._scaleCurve);
    }

    private void OnDisable()
    {
        this.DOKill();
        _TMP.DOKill();
        _rectTransform.DOKill();
    }
}
