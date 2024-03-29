using UnityEngine;

public class OnBulletHitParticle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnBulletHitEvent _onBulletHitEvent;

    [Header("Data")]
    [SerializeField] private OnBulletHitParticleData _data;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        _onBulletHitEvent.onHit += SpawnParticle;
    }

    private void OnDisable()
    {
        _onBulletHitEvent.onHit -= SpawnParticle;
    }

    private void SpawnParticle(Collision2D collisionData)
    {
        var hitContact = collisionData.GetContact(0);

        var particle = _objectPooler.GetFromPool(_data.particle, hitContact.point, Quaternion.identity);

        particle.transform.right = hitContact.normal * -1;
    }
}