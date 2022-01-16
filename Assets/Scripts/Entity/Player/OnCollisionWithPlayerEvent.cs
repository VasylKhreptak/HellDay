using System;
using DG.Tweening;
using UnityEngine;

public class OnCollisionWithPlayerEvent : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private OnCollisionWithPlayerEventData _data;
    
    public Action<Collision2D> onCollision;

    private bool _canInteract = true;

    private Tween _waitTween;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_canInteract && 
            _data.playerLayerMask.ContainsLayer(col.gameObject.layer))
        {
            onCollision?.Invoke(col);
            
            ControlInteractSpeed();
        }
    }

    private void ControlInteractSpeed()
    {
        _canInteract = false;
        _waitTween = this.DOWait(_data.InteractDelay).OnComplete(() => { _canInteract = true; });
    }

    private void OnDestroy()
    {
        _waitTween.Kill();
    }
}
