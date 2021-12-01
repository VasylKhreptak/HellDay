using System;
using System.Collections;
using UnityEngine;

public class HumanDetection : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")] 
    [SerializeField] private float _checkDelay = 1;
    [SerializeField] private float _rescueRadius = 3f;

    [Header("Humans")] 
    [SerializeField] private Transform[] _humans;
    
    private Coroutine _checkCoroutine;
    public Transform _closestHuman { get; private set; }

    public static Action<bool> onHumanNear;
    
    private void Awake()
    {
        StartCoroutine(CheckRoutine());
    }

    private IEnumerator CheckRoutine()
    {
        while (true)
        {
            _closestHuman = GetClosestHuman();

            if (_closestHuman != null)
            {
                bool canShowBtn = _transform.position.ContainsPosition(_rescueRadius, _closestHuman.position);

                onHumanNear?.Invoke(canShowBtn);
            }

            yield return new WaitForSeconds(_checkDelay);
        }
    }

    public void ResqueHuman()
    {
        Transform human = _closestHuman;

        if (human != null)
        {
            Destroy(human.gameObject);
            
            Debug.Log("Added resqued human to the score!");
        }
    }

    private Transform GetClosestHuman()
    {
        return _transform.FindClosestTransform(_humans);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_transform == null) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_transform.position, _rescueRadius);
    }
}
