using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTouchCounterReachReloadScene : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private OnTouchCounterReachReloadSceneData _data;
    
    [Header("References")]
    [SerializeField] private TouchCounterEvent _touchCounterEvent;

    private Tween _waitTween;
    
    private void OnEnable()
    {
        _touchCounterEvent.onReachCount += ReloadScene;
    }

    private void OnDisable()
    {
        _touchCounterEvent.onReachCount -= ReloadScene;
    }

    private void ReloadScene()
    {
        _waitTween = this.DOWait(_data.ReloadDelay).OnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void OnDestroy()
    {
        _waitTween.Kill();
    }
}
