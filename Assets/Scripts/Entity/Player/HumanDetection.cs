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

    private void Awake()
    {
        StartCoroutine(CheckRoutine());
    }

    private IEnumerator CheckRoutine()
    {
        while (true)
        {
            _closestHuman = GetClosestHuman();
            
            bool canShowBtn = _transform.ContainsTransform(_rescueRadius, _closestHuman); 
            
            Messenger<bool>.Broadcast(GameEvent.ANIMATE_SAVE_HUMAN_BUTTON, canShowBtn);
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
