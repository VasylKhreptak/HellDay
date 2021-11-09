using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;
    [SerializeField] private Pools _destoyParticle = Pools.WoodItemDestroyParticle;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void  OnDestroy()
    {
        if (gameObject.scene.isLoaded == false) return;
        
        _objectPooler.GetFromPool(_destoyParticle, _transform.position, Quaternion.identity);
        
        Destroy(_transform.parent.gameObject);
    }
}
