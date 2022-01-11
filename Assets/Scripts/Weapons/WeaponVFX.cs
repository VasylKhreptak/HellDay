using TMPro;
using UnityEngine;

public class WeaponVFX : MonoBehaviour
{
    [Header("Empty Ammo Sound ")]
    [SerializeField] private AudioClip _emptyAmmo;

    private ObjectPooler _objectPooler;
    private AudioPooler _audioPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        _audioPooler = AudioPooler.Instance;
    }

    public void PlayShootAudio(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void TriggerShootAnimation(Animator animator, int id)
    {
        animator.SetTrigger(id);
    }

    public void SpawnBulletMuff(Pools type, Vector2 position, Quaternion rotation)
    {
        _objectPooler.GetFromPool(type, position, rotation);
    }

    public void SpawnShootSmoke(Pools type, Vector2 position, Quaternion rotation)
    {
        var pooledObject = _objectPooler.GetFromPool(type, position, rotation);

        pooledObject.transform.localScale = new Vector3(PlayerFaceDirectionController.FaceDirection, 1, 1);
    }

    public void SpawnShootSparks(Pools type, Vector2 position, Quaternion rotation)
    {
        var pooledObject = _objectPooler.GetFromPool(type, position, rotation);

        pooledObject.transform.localScale = new Vector3(PlayerFaceDirectionController.FaceDirection, 1, 1);
    }

    public void PlayEmptyAmmoSound(Vector3 position)
    {
        _audioPooler.PlayOneShootSound(AudioMixerGroups.VFX, _emptyAmmo, position, 1f, 1f);
    }
}