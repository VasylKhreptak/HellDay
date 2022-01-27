using UnityEngine;

public class UI_RandomSprite : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteAdapter _spriteAdapter;
        
    [Header("Preferences")]
    [SerializeField] private Sprite[] _sprites;

    private void OnEnable()
    {
        _spriteAdapter.sprite = _sprites.Random();
    }
}