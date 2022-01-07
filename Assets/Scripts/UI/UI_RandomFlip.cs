using UnityEngine;

public class UI_RandomFlip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        FlipRandomly();
    }

    private void FlipRandomly()
    {
        _spriteRenderer.flipX = Extensions.Random.Bool();
        _spriteRenderer.flipY = Extensions.Random.Bool();

    }
}
