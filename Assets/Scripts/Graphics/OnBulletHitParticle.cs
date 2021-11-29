using System;
using UnityEngine;

public class OnBulletHitParticle : MonoBehaviour
{
    [Header("Data")] 
    [SerializeField] private OnBulletHitParticleData _data;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_data.bulletLayerMask.ContainsLayer(other.gameObject.layer))
        {
            SpawnParticle(other);
        }
    }

    private void SpawnParticle(Collision2D collisionData)
    {
        ContactPoint2D hitContact = collisionData.GetContact(0);              
        
        GameObject particle = _objectPooler.GetFromPool(_data.particle, hitContact.point,Quaternion.identity);

        particle.transform.right = hitContact.normal * -1;
    }
}
