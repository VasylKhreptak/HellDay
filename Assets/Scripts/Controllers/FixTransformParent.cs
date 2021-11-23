using DG.Tweening;
using UnityEngine;

public class FixTransformParent : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    private Transform _previousParent;

    private void Awake()
    {
        this.DOWait(0).OnComplete(() =>
        {
            _previousParent = _transform.parent;
            
            Debug.Log("Previous parent: " + (_previousParent.name));
        });
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded == false) return;

        _transform.parent = null;
    }
}
