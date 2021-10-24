using DG.Tweening;
using UnityEngine;

public class DestroyableObject : PhysicalObject
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    
    [Header("Preferences")]
    [SerializeField] protected float _minDamageImpulse = 100f;
    [SerializeField] protected Pools _destroyParticle;
    
    protected ObjectPooler _objectPooler;

    public Transform Transform => _transform;

    protected override void Start()
    {
        SetMaxDurability(_maxDurability);

        _objectPooler = ObjectPooler.Instance;
    }

    protected void OnCollisionEnter2D(Collision2D collision2D)
    {
        float impulse = GetCollisionImpulse(collision2D);

        if (collision2D.collider.CompareTag("Bullet") ||
            impulse > _minDamageImpulse)
        {
            TakeDamage(1f);
        }                                                                                
    }

    public  override void DestroyActions()
    {
        _objectPooler.GetFromPool(_destroyParticle, _transform.position, Quaternion.identity);

        DOTween.Kill(gameObject);
        
        Destroy(gameObject);
    }

    protected float GetCollisionImpulse(Collision2D collision2D)
    {
        float impulse = 0f;

        for (int i = 0; i < collision2D.contacts.Length; i++)
        {
            impulse += collision2D.contacts[i].normalImpulse;
        }

        return impulse;
    }
}