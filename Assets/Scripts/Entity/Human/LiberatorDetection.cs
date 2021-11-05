using System;
using System.Collections;
using UnityEngine;

public class LiberatorDetection : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _player;
    [SerializeField] private float _checkDelay = 1.5f;
    [SerializeField] private ResqueSignAnimation _signAnimation;

    [Header("Preferences")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private float _detectRadius = 10f;

    private void Awake()
    {
        StartDetection();
    }

    private void StartDetection()
    {
        StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            _signAnimation.SetSignState(_transform.ContainsTransform(_detectRadius, _player));

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
