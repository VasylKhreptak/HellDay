using UnityEngine;
using System.Collections;

public class TargetDetection : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    
    [Header("Targets")]
    [SerializeField] private Transform[] _targets;

    [Header("Preferences")] 
    [SerializeField] private float _findTargetDelay = 1;

    public Transform closestTarget { get; private set; }

    private void Awake()
    {
        StartCoroutine(FindClosestTargetRoutine());
    }

    private IEnumerator FindClosestTargetRoutine()
    {
        while (true)
        {
            closestTarget = _transform.FindClosestTransform(_targets);
            
            yield return new WaitForSeconds(_findTargetDelay);
        }   
    }
}
