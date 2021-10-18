using DG.Tweening;
using UnityEngine;

public class DoTweenPreferences : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private int _tweensCapacity = 500;
    [SerializeField] private int _sequenceCapacity = 200;

    private void Awake()
    {
        DOTween.SetTweensCapacity(_tweensCapacity, _sequenceCapacity);
    }
}