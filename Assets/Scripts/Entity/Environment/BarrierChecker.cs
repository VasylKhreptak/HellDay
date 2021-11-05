using UnityEngine;

public class BarrierChecker : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private LayerMask _layerMask;
    
    public bool isBarrierClose { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            isBarrierClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isBarrierClose = false;
    }
}