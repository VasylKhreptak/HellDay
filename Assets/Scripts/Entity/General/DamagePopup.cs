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

    private Tween _tween1, _tween2, _tween3;
    
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
        
        _tween1 = this.DOWait(_data.Lifetime).OnComplete(() => { gameObject.SetActive(false);});
        _tween2 = _TMP.DOFade(0, _data.Lifetime).SetEase(_data._alphaCurve);
        _tween3 = _rectTransform.DOScale(Vector3.zero, _data.Lifetime).SetEase(_data._scaleCurve);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false) return;
        
        KillTweens();
    }

    private void KillTweens()
    {
        _tween1.Kill();
        _tween2.Kill();
        _tween3.Kill();
    }
}
