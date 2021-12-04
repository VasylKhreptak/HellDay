using UnityEngine;
using GrassState = GrassDestroyData.GrassState;

public class GrassDestroy : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DamageableObject _damageableObject;

    [Header("Data")] 
    [SerializeField] private GrassDestroyData _data;

    private ObjectPooler _objectPooler;
    private AudioPooler _audioPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioPooler = AudioPooler.Instance;
    }

    private void OnEnable()
    {
        _damageableObject.onTakeDamage += ProcessDamage;
    }

    private void OnDisable()
    {
        _damageableObject.onTakeDamage -= ProcessDamage;
    }

    private void ProcessDamage(float damage)
    {
        if (TryFindAppropriateState(_data.grassStates, out GrassState state))
        {
            SetGrassState(state);

            SpawnDamageParticle();

            PlayChagneStateAudio();
        }
        
    }

    private void SetGrassState(GrassState grass)
    {
        _spriteRenderer.sprite = grass.sprite;
        _spriteRenderer.material = grass.shaderMaterial;
    }

    private bool TryFindAppropriateState(GrassState[] states, out GrassState state)
    {
        state = null; 
        
        if (states.Length == 0) return false;

        foreach (var potentialState in states)
        {
            if (_damageableObject.Health < (potentialState.healthPercentage / 100f * _damageableObject.MAXHealth))
            {
                state = potentialState;
            }
        }

        if (state == null || state.sprite == _spriteRenderer.sprite) return false;

        return true;
    }

    private void SpawnDamageParticle()
    {
        _objectPooler.GetFromPool(_data.damageEffect, _transform.position, Quaternion.identity);
    }

    private void PlayChagneStateAudio()
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _data._stateChangeAudioClips.Random(),
            _transform.position, 1f, 1f);
    }
}
