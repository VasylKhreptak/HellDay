using DG.Tweening;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _lifeTime = 10f;

    private Tween _tween;
    
    private void Update()
    {
        _transform.Translate(new Vector3(-_speed, 0, 0));
    }
    
    private void OnEnable()
    {
        _tween = this.DOWait(_lifeTime).OnComplete(() => { gameObject.SetActive(false); });
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}
