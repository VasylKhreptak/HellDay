using System;
using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    public bool isObstacleClose { get; private set; }

    private void OnTriggerStay2D(Collider2D other)
    {
        isObstacleClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isObstacleClose = false;
    }
}
