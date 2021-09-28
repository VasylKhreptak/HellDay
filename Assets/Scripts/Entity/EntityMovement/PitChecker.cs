using System;
using UnityEngine;

public class PitChecker : MonoBehaviour
{
    public bool isPitNearp { get;private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPitNearp = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPitNearp = true;
    }
}
