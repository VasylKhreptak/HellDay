using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    public bool isObstacleClose { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isObstacleClose = false;
        }
        else
        {
            isObstacleClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isObstacleClose = false;
    }
}