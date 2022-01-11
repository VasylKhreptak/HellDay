using DG.Tweening;
using UnityEngine;

public class DoTweenSettings : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private int _maxTweens = 500;
    [SerializeField] private int _maxSequences = 200;

    private void Awake()
    {
        DOTween.SetTweensCapacity(_maxTweens, _maxSequences);
    }
}