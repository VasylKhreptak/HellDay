using UnityEngine;

public class OnTouchCounterReachDisable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TouchCounterEvent _touchCounterEvent;

    private void OnEnable()
    {
        _touchCounterEvent.onReachCount += Disable;
    }

    private void OnDisable()
    {
        _touchCounterEvent.onReachCount -= Disable;
    }

    private void Disable() => gameObject.SetActive(false);
}
