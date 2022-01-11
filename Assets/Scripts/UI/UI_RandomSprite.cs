using UnityEngine;

public class UI_RandomSprite : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Preferences")]
    [SerializeField] private Sprite[] _sprites;

    private void OnEnable()
    {
        _spriteRenderer.sprite = _sprites.Random();
    }
}