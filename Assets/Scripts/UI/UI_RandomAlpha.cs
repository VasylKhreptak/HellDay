using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class UI_RandomAlpha : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Data")]
    [SerializeField] private UI_RandomAlphaData _data;


    private void OnEnable()
    {
        SetRandomAlpha();
    }

    private void SetRandomAlpha()
    {
        _spriteRenderer.color = _spriteRenderer.color.WithAlpha(Random.Range(_data.MinAlpha, _data.MaxAlpha));
    }
}
