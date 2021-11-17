using System;
using System.Collections;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private Transform _missileSpawnPlace;
    [SerializeField] private Transform _target;
    
    [Header("Prefereces")] 
    [SerializeField] private Pools _missile;
    [SerializeField] private WeaponAmmo _weaponAmmo;
    [SerializeField] private float _shootDelay = 1f;

    [Header("Camera shake")] 
    [SerializeField] private float _maxCameraShakeIntensity;
    [SerializeField] private float _cameraShakeDuration = 0.5f;

    [Header("Taregt detection preferences")]
    [SerializeField] private float _checkRange = 9f;

    [Header("Shoot Audio Clips")] 
    [SerializeField] private AudioClip _shootAudioClip;

    private Coroutine _shootCoroutine;
    private ObjectPooler _objectPooler;
    private AudioPooler _audioPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioPooler = AudioPooler.Instance;
        
        StartCoroutine(ControlShootRoutine());
    }

    private IEnumerator ControlShootRoutine()
    {
        while (true)
        {
            if (CanShoot())
            {
                Shoot();
            }

            yield return new WaitForSeconds(_shootDelay);
        }
    }

    private bool CanShoot()
    {
        return _weaponAmmo.IsEmpty == false && 
               _transform.position.ContainsPosition(_checkRange, _target.position) && _target.gameObject.activeSelf;
    }

    private void Shoot()
    {
        _weaponAmmo.GetAmmo();
        
        Messenger<Vector3, float, float>.Broadcast(GameEvents.SHAKE_CAMERA, _transform.position, 
            _maxCameraShakeIntensity, _cameraShakeDuration);

        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _shootAudioClip, _missileSpawnPlace.position,
            1f, 1f);
        
        _objectPooler.GetFromPool(_missile, _missileSpawnPlace.position, _missileSpawnPlace.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _target == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _checkRange);
        Gizmos.DrawLine(_transform.position, _target.position);
    }
}
