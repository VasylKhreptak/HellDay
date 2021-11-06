using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform _bloodSpawnPlace;

    [Header("Preferences")] [SerializeField]
    private Pools _blood;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damageDelay;
    [SerializeField] private float _damage;

    [Header("Audio Clips")] [SerializeField]
    private AudioClip[] _damageClips;

    private Coroutine _damageCoroutine;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer) &&
            other.gameObject.TryGetComponent<KillableTarget>(out KillableTarget target))
        {
            StartDamage(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopDamage();
    }

    private void StartDamage(KillableTarget target)
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DamageRoutine(target));
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

    private IEnumerator DamageRoutine(KillableTarget target)
    {
        while (true)
        {
            target.Killable.TakeDamage(_damage);
            
            SpawnBlood();

            PlayDamageSound();
            
            yield return new WaitForSeconds(_damageDelay);
        }
    }

    private void SpawnBlood()
    {
        _objectPooler.GetFromPool(_blood, _bloodSpawnPlace.position, Quaternion.identity);
    }

    private void PlayDamageSound()
    {
        RandomAudio.Play(_bloodSpawnPlace.position, _damageClips);
    }
}
