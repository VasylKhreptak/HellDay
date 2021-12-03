using System.Collections;
using UnityEngine;

public class LiberatorDetection : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _playerObj;
    [SerializeField] private float _checkDelay = 1.5f;
    [SerializeField] private ResqueSignAnimation _signAnimation;

    [Header("Preferences")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private float _detectRadius = 10f;

    private Coroutine _detectionCoroutine;

    private void Awake()
    {
        StartDetection();
    }

    private void OnEnable()
    {
        Player.onPlayerDied += StopDetection;
        Player.onResurrection += StartDetection;
    }

    private void OnDisable()
    {
        Player.onPlayerDied -= StopDetection;
        Player.onResurrection -= StartDetection;
    }

    private void StartDetection()
    {
        if (_detectionCoroutine == null)
        {
            _detectionCoroutine = StartCoroutine(DetectionRoutine());
        }
    }

    private void StopDetection()
    {
        if (_detectionCoroutine != null)
        {
            StopCoroutine(_detectionCoroutine);

            _detectionCoroutine = null;

            _signAnimation.SetSignState(false);
        }
    }

    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            if (_playerObj.activeSelf)
            {
                _signAnimation.SetSignState(_transform.position.ContainsPosition(_detectRadius, 
                    _player.position));
            }

            yield return new WaitForSeconds(_checkDelay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_transform.position, _detectRadius);
    }
}
