using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DamageableTarget))]
public class SpikesInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private DamageableTarget _damageableTarget;

    [Header("Data")]
    [SerializeField] private SpikeInteractData _data;

    private Coroutine _damageCoroutine;
    private ObjectPooler _objectPooler;
    private AudioPooler _audioPooler;
    
    private void Start()
    {
        _audioPooler = AudioPooler.Instance;
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes") &&
            gameObject.activeSelf)
        {
            StartDamaging();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            StopDamaging();
        }
    }

    private void StartDamaging()
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DamageRoutine());
        }
    }

    private void StopDamaging()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);

            _damageCoroutine = null;
        }
    }

    private IEnumerator DamageRoutine()
    {
        while (true)
        {
            _damageableTarget.Damageable.TakeDamage(Random.Range(_data.MINDamage, _data.MAXDamage));

            PlayDamageSound();

            SpawnBlood();

            yield return new WaitForSeconds(_data.DamageDelay);
        }
    }

    private void PlayDamageSound()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data.biteAudioClips.Random(),
            _transform.position, 1f, 1f);
    }

    private void SpawnBlood()
    {
        _objectPooler.GetFromPool(_data.blood, _transform.position, Quaternion.identity);
    }
}