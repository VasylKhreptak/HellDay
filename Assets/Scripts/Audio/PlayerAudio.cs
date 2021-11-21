using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAudio : MonoBehaviour
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

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _legKickAudioClips;
    [SerializeField] private AudioClip[] _boneCrackAudioClips;

    [Header("Step Audio")] 
    [SerializeField] private StepAudio[] _stepAudios;
    [SerializeField] private AudioClip[] _defaultStepAudios;
    
    private AudioPooler _audioPooler;
    
    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
    }
    
    public void PlayLegKickSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _legKickAudioClips.Random(),
            _transform.position, 1f, 1f);
    }

    public void PlayBoneCrackSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _boneCrackAudioClips.Random(),
            _transform.position, 1f, 1f);
    }

    public void PlayStepSound()
    {
        if (TryGetSteppedSurfaceType(out SurfaceTypes? steppedSurface))
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

   private bool TryGetSteppedSurfaceType(out SurfaceTypes? surfaceType)
    {
        SurfaceTypeTile surfaceTypeTile = _tilemap.
            GetTile<SurfaceTypeTile>(_tilemap.WorldToCell(_transform.position - 
                                                          new Vector3(0, 1.5f, 0)));

        if (surfaceTypeTile == null)
        {
            surfaceType = null;
        }
        else
        {
            surfaceType = surfaceTypeTile.surfaceType;
        }
        
        return surfaceTypeTile != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _tilemap == null) return;
        
        Vector3Int tilePos = _tilemap.WorldToCell(_transform.position -
                                                  new Vector3(0, 1f, 0.0f));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_tilemap.CellToWorld(tilePos), 0.5f);
    }
}
