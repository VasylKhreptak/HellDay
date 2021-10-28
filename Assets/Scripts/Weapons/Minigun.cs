using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Minigun : WeaponCore, IWeapon
{
    [Header("Preferences")] 
    [SerializeField] private float _spinTime = 2f;

    [Header("Audio Clips")] 
    [SerializeField] private AudioClip _windUp;
    [SerializeField] private AudioClip _windDown;
    [SerializeField] private AudioClip _spinLoop;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _shootStop;
    
    protected static readonly int SpinTrigger = Animator.StringToHash("Spin");

    private bool _isSpinning;
    private bool _isShooting;

    private Coroutine _playSpinAudio;
    private Coroutine _stopShootAudio;
    
    public new void StartShooting()
    {
        if (_shootCoroutine != null || CanShoot() == false) return;

        StartPlayingSpinAudio(() =>
        {
            _shootCoroutine = StartCoroutine(Shoot());

            StartCoroutine(ControlShootSpeed());
        });
    }

    public new void StopShooting()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
        
        StopPlayingSpinAudio();
        
        StopShootAudio();
        
        FadeSpin();
        
        _shootCoroutine = null;
    }

    protected override void OnLegPunched(float time)
    {
        StopShooting();
        
        base.OnLegPunched(time);
    }

    private void StartPlayingSpinAudio(Action onSpinEnd)
    {
        if(_playSpinAudio == null)
        {
            _playSpinAudio = StartCoroutine(PlaySpin(onSpinEnd));
        }
    }

    private void StopPlayingSpinAudio()
    {
        if (_playSpinAudio != null)
        {
            StopCoroutine(_playSpinAudio);

            _playSpinAudio = null;
        }
    }

    private void FadeSpin()
    {
        this.DOWait(1).OnComplete(() =>
        {
            _animator.SetBool(SpinTrigger, false);
        }).SetId("MinigunSpin");
    }

    private void PlayAudioClip(AudioSource audioSource, AudioClip audioClip, bool loop = false)
    {
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    protected override IEnumerator Shoot()
    {
        Messenger.Broadcast(GameEvent.PLAYED_AUDIO_SOURCE, MessengerMode.DONT_REQUIRE_LISTENER);

        _isShooting = true;

        PlayAudioClip(_audioSource, _shootSound, true);
        
        _animator.SetBool(SpinTrigger, false);

        while (true)
        {
            if (CanShoot())
            {
                ShootActions();
            }
            else if(_ammo.IsEmpty)
            {
                StopShooting();
            }

            yield return new WaitForSecondsRealtime(_shootDelay);
        }
    }

    protected override void ShootActions()
    {
        SpawnBullet();
        _ammo.GetAmmo();
        _weaponVFX.SpawnBulletMuff(_bulletMuff, _bulletMuffSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSmoke(Pools.ShootSmoke, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.SpawnShootSparks(Pools.ShootSparks, _shootParticleSpawnPlace.position, Quaternion.identity);
        _weaponVFX.StartShootAnimation(_animator, ShootTrigger);
    }

    private IEnumerator PlaySpin(Action onSpinEnd)
    {
        DOTween.Kill("MinigunSpin");
        _animator.SetBool(SpinTrigger, true);
        
        _isSpinning = true;
        
        PlayAudioClip(_audioSource, _windUp);

        yield return new WaitForSeconds(_windUp.length);
        
        PlayAudioClip(_audioSource, _spinLoop, true);

        yield return new WaitForSeconds(_spinTime);

        onSpinEnd.Invoke();
    }
    
    private void  StopShootAudio()
    {
        if (_isShooting)
        {
            PlayAudioClip(_audioSource, _shootStop);
        }
        else if (_isSpinning)
        {
            PlayAudioClip(_audioSource, _windDown);
        }

        _isShooting = false;
        _isSpinning = false;
    }
}