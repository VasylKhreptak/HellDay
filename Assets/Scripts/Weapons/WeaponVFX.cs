using UnityEngine;

public class WeaponVFX : MonoBehaviour
{
    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void PlayShootAudio(AudioSource audioSource)
    {
        audioSource.Play();
    }
    
    public void StartShootAnimation(Animator animator, int id)
    {
        animator.SetTrigger(id);
    }

    public void SpawnBulletMuff(Pools type, Vector2 position, Quaternion rotation)
    {
        _objectPooler.GetFromPool(type, position, rotation);
    }

    public void SpawnShootSmoke(Pools type, Vector2 position, Quaternion rotation)
    {
        GameObject pooledObject = _objectPooler.GetFromPool(type, position, rotation );

        pooledObject.transform.localScale = new Vector3(PlayerMovement.MovementDirection, 1, 1);
    }

    public void SpawnShootSparks(Pools type, Vector2 position, Quaternion rotation)
    {
        GameObject pooledObject = _objectPooler.GetFromPool(type, position, rotation);

        pooledObject.transform.localScale = new Vector3(PlayerMovement.MovementDirection, 1, 1);
    }
}
