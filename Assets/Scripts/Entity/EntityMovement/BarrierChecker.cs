using System.Collections;
using UnityEngine;

public class BarrierChecker : MonoBehaviour
{
    public bool isBarrierClose { get; private set; } = false;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isBarrierClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isBarrierClose = false;
    }
}