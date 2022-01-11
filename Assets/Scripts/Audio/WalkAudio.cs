using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkAudio : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WalkAudioData _data;

    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private GroundChecker _groundChecker;

    private Tilemap _tilemap;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
        _tilemap = GameAssets.Instance.mainTilemap;
    }

    public void PlayStepSound()
    {
        if (_tilemap.TryGetSurfaceType(out var steppedSurface,
                _transform.position - new Vector3(0, 1, 0)))
        {
            foreach (var stepAudio in _data.stepAudios)
                if (stepAudio.surfaceTypes == steppedSurface)
                {
                    _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, stepAudio.audioClips.Random(),
                        _transform.position, stepAudio.volume, 1f);

                    return;
                }
        }
        else if (_groundChecker.IsGrounded())
        {
            _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data.defaultStepAudios.Random(),
                _transform.position, 1f, 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _tilemap == null) return;

        var tilePos = _tilemap.WorldToCell(_transform.position -
                                           new Vector3(0, 1f, 0.0f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_tilemap.CellToWorld(tilePos) + new Vector3(0.5f, 0.5f, 0), Vector3.one);
    }
}