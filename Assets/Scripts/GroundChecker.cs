using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}
