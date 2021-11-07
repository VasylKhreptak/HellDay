using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KillableTarget))]
public class SpikesInteract : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private KillableTarget _killableTarget;

    [Header("Preferences")] 
    [SerializeField] private float _damageDelay = 0.5f;
    [SerializeField] private float _damage = 15f;
    [SerializeField] private Pools _blood;

    [Header("Damage Audio Clips")] 
    [SerializeField] private AudioClip[] _damageAudioClips;

    private Coroutine _damageCoroutine;
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spikes"))
        {
            StartDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopDamage();
    }

    private void StartDamage()
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DamageRoutine());
        }
    }

    private void StopDamage()
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
            _killableTarget.Killable.TakeDamage(_damage);
            
            PlayDamageSound();
            
            SpawnBlood();
            
            yield return new WaitForSeconds(_damageDelay);
        }
    }

    private void PlayDamageSound()
    {
        RandomAudio.Play(_transform.position, _damageAudioClips);
    }

    private void SpawnBlood()
    {
        _objectPooler.GetFromPool(_blood, _transform.position, Quaternion.identity);
    }
}
