using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
