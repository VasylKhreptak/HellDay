using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UI_HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI_SlideAnimation[] _slideAnimations;

    [Header("Preferences")]
    [SerializeField] private float _startupShowDelay = 0.5f;
    [SerializeField] private float _hideDelay = 1f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startupShowDelay);

        ShowHUDElements();
    }

    private void OnEnable()
    {
        Player.onDie += () => { this.DOWait(_hideDelay).OnComplete(() => { HideHUDElements(); }); };
    }

    private void OnDisable()
    {
        Player.onDie -= () => { this.DOWait(_hideDelay).OnComplete(() => { HideHUDElements(); }); };
    }

    private void ShowHUDElements()
    {
        foreach (var slideAnimation in _slideAnimations) slideAnimation.Show();
    }

    private void HideHUDElements()
    {
        foreach (var slideAnimation in _slideAnimations) slideAnimation.Hide();
    }
}