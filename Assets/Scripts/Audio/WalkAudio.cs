using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkAudio : MonoBehaviour
{
    [System.Serializable]
    private class StepAudio
    {
        public SurfaceTypes surfaceTypes;
        public AudioClip[] audioClips;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private Tilemap _tilemap;
    
    [Header("Preferences")] 
    [SerializeField] private StepAudio[] _stepAudios;
    [SerializeField] private AudioClip[] _defaultStepAudios;

    private AudioPooler _audioPooler;

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }

    public void PlayStepSound()
    {
        if (_tilemap.TryGetSurfaceType(out SurfaceTypes? steppedSurface, _transform.position - new Vector3(0, 1, 0)))
        {
            foreach (var stepAudio in _stepAudios)
            {
                if (stepAudio.surfaceTypes == steppedSurface)
                {
                    _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, stepAudio.audioClips.Random(),
                        _transform.position, stepAudio.volume, 1f);

                    return;
                }
            }
        }
        else
        {
            _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _defaultStepAudios.Random(),
                _transform.position, 1f, 1f);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _tilemap == null) return;
        
        Vector3Int tilePos = _tilemap.WorldToCell(_transform.position -
                                                  new Vector3(0, 1f, 0.0f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_tilemap.CellToWorld(tilePos) + new Vector3(0.5f, 0.5f, 0), Vector3.one);
    }
}
