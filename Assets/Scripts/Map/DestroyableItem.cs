using DG.Tweening;
using UnityEngine;

public class DestroyableItem : PhysicalItem
{
    [Header("Preferences")] 
    [SerializeField] protected Transform _transform;
    [SerializeField] protected float _minDestroyImpulse = 350f;
    [SerializeField] protected float _minDamageImpulse = 100f;
    [SerializeField] protected Pools _destroyParticle;

    protected ObjectPooler _objectPooler;

    protected override void Start()
    {
        SetMaxDurability(_maxDurability);

        _objectPooler = ObjectPooler.Instance;
    }

    protected void OnCollisionEnter2D(Collision2D collision2D)
    {
        float impulse = GetCollisionImpulse(collision2D);

        if (collision2D.collider.CompareTag("Bullet") == true ||
            impulse > _minDamageImpulse)
        {
            TakeDamage(1f);
        }

        if (impulse > _minDestroyImpulse)
        {
            DestroyActions();
        }
    }

    protected override void DestroyActions()
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