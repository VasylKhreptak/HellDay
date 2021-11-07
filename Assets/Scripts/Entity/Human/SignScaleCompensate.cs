using UnityEngine;

public class SignScaleCompensate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _signRenderer;

    public void OnScaleChanged(int direction)
    {
        _signRenderer.flipX = direction == -1;
    }
}
