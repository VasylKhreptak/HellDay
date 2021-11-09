using UnityEngine;

public class DestroyableObject : PhysicalObject
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    
    [Header("Preferences")]
    [SerializeField] protected float _minDamageImpulse = 100f;
    [SerializeField] protected bool _canBeDestroyed = true;
    
    protected ObjectPooler _objectPooler;

    public Transform Transform => _transform;

    protected override void Start()
    {
        SetMaxDurability(_maxDurability);

        _objectPooler = ObjectPooler.Instance;
    }

    protected void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag("Bullet") ||
            collision2D.GetImpulse() > _minDamageImpulse)
        {
            TakeDamage(1f);
        }                                                                                
    }

    public  override void DestroyActions()
    {
        if (_canBeDestroyed)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}