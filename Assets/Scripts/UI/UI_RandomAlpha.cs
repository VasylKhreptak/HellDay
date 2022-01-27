using UnityEngine;
using Random = UnityEngine.Random;

public class UI_RandomAlpha : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ColorAdapter _colorAdapter;

    [Header("Data")]
    [SerializeField] private UI_RandomAlphaData _data;


    private void OnEnable()
    {
        SetRandomAlpha();
    }

    private void SetRandomAlpha()
    {
        _colorAdapter.color = _colorAdapter.color.WithAlpha(Random.Range(_data.MinAlpha, _data.MaxAlpha));
    }
}