using DG.Tweening;
using UnityEngine;

public class UI_BillboardAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _billboard;
    [SerializeField] private UI_FadeAnimation[] _fadeAnimations;

    [Header("Preferences")]
    [SerializeField] private float _showDelay = 0.5f;

    private void OnEnable()
    {
        _billboard.SetActive(false);
        
        this.DOWait(_showDelay).OnComplete(ShowBillboard);
    }

    private void ShowBillboard()
    {
        _billboard.SetActive(true);

        UI_FadeAnimation.Animate(_fadeAnimations, true);
    }
}
