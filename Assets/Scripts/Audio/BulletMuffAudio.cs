using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletMuffAudio : MonoBehaviour
{
    [System.Serializable]
    private class MuffAudio
    {
        public SurfaceTypes surfaceType;
        [Range(1f, 100f)] public float volume = 1f;
    }

    [System.Serializable]
    private class PlaySoundProbability
    {
        public Weapons weaponType;
        [Range(0f, 100f)] public float probability;
    }

    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Preferences")] 
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private MuffAudio[] _muffAudios;
    [SerializeField] private PlaySoundProbability[] _playSoundProbabilities;
    [SerializeField] private float _tileDetectLength = 0.3f;
        
    private AudioPooler _audioPooler;
    private Tilemap _tilemap;
    private bool _playedAudio;
    private PlayerWeaponControl _playerWeaponControl;

    private void Awake()
    {
        _tilemap = FindObjectsOfType<Tilemap>()[1];
    }

    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
        _playerWeaponControl = PlayerWeaponControl.Instance;
    }

    private void OnEnable()
    {
        _playedAudio = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CanPlaySound() &&
            _tilemap.TryGetSurfaceType(out SurfaceTypes? fallenSurface, _transform.position -
                                                                        new Vector3(0, _tileDetectLength, 0)))
        {
            foreach (var muffAudio in _muffAudios)
            {
                if (muffAudio.surfaceType == fallenSurface)
                {
                    _audioPooler.PlayOneShootSound(AudioMixerGroups.WEAPON, _audioClips.Random(),
                        _transform.position, muffAudio.volume, 1f);
                    _playedAudio = true;
                }
            }
        }
    }

    private bool CanPlaySound()
    {
        return _playedAudio == false &&
               Probability.GetBoolean(GetSoundProbability());
    }

    private float GetSoundProbability()
    {
        Weapons curentWeapon = _playerWeaponControl.currentWeapon.WeaponType;

        foreach (var sound in _playSoundProbabilities)
        {
            if (sound.weaponType == curentWeapon)
            {
                return sound.probability;
            }
        }

        return 0;
    }

    private void OnDrawGizmosSelected()
    {
        if(_transform == null) return; 
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_transform.position, _transform.position - new Vector3(0, _tileDetectLength, 0));
    }
}