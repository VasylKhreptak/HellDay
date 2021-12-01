using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletMuffAudio : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    [Header("Bullet Muff Audio Data")]
    [SerializeField] private BulletMuffAudioData _data;
    
    private AudioPooler _audioPooler;
    private Tilemap _tilemap;
    private bool _playedAudio;
    private PlayerWeaponControl _playerWeaponControl;
    
    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
        _playerWeaponControl = GameAssets.Instance.playerWeaponControl;
        _tilemap = GameAssets.Instance.mainTilemap;
    }

    private void OnEnable()
    {
        _playedAudio = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CanPlaySound() &&
            _tilemap.TryGetSurfaceType(out SurfaceTypes? fallenSurface, _transform.position -
                                                                        new Vector3(0, _data.TileDetectLength, 0)))
        {
            foreach (var muffAudio in _data.muffAudios)
            {
                if (muffAudio.surfaceType == fallenSurface)
                {
                    _audioPooler.PlayOneShootSound(AudioMixerGroups.WEAPON, _data.audioClips.Random(),
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
        Weapons curentWeapon = _playerWeaponControl.currentWeapon.weaponType;
        
        foreach (var sound in _data.playSoundProbabilities)
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
        Gizmos.DrawLine(_transform.position, _transform.position - new Vector3(0, _data.TileDetectLength, 0));
    }
}