using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    [Header("Preferences")] 
    [SerializeField] private LayerMask _layerMask;

    public bool isObstacleClose { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            isObstacleClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isObstacleClose = false;
    }
}