using System.Collections;
using UnityEngine;

public class BarrierChecker : MonoBehaviour
{
    public bool isBarrierClose { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isBarrierClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isBarrierClose = false;
    }
}