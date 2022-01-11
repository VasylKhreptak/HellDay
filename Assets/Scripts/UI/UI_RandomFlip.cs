using UnityEngine;

public class UI_RandomFlip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Preferences")]
    [SerializeField] private bool _x = true;
    [SerializeField] private bool _y = true;

    private void OnEnable()
    {
        FlipRandomly();
    }

    private void FlipRandomly()
    {
        if (_x) _spriteRenderer.flipX = Extensions.Random.Bool();

        if (_y) _spriteRenderer.flipY = Extensions.Random.Bool();
    }
}