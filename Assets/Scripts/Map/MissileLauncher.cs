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
    [SerializeField] private WeaponAmmo _weaponAmmo;

    [Header("Data")]
    [SerializeField] private MissileLauncherData _data;
    
    private Coroutine _shootCoroutine;
    private ObjectPooler _objectPooler;
    private AudioPooler _audioPooler;

    public static Action<Vector3, float, float> onCameraShake;

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

            yield return new WaitForSeconds(_data.ShootDelay);
        }
    }

    private bool CanShoot()
    {
        return _weaponAmmo.IsEmpty == false && 
               _transform.position.ContainsPosition(_data.CheckRange, _target.position) && _target.gameObject.activeSelf;
    }

    private void Shoot()
    {
        _weaponAmmo.GetAmmo();
        
        onCameraShake.Invoke(_transform.position, _data.MAXCameraShakeIntensity, _data.CameraShakeDuration);

        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data._missileLaunchSound, _missileSpawnPlace.position,
            1f, 1f);
        
        _objectPooler.GetFromPool(_data.missile, _missileSpawnPlace.position, _missileSpawnPlace.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || _target == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transform.position, _data.CheckRange);
        Gizmos.DrawLine(_transform.position, _target.position);
    }
}
