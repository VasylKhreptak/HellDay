using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerDownParticle : MonoBehaviour, IPointerDownHandler
{
    [Header("Preferences")]
    [SerializeField] private Pools _particle = Pools.WoodItemDestroyParticle;

    private ObjectPooler _objectPooler;

    private void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var touchPos = eventData.pointerPressRaycast.worldPosition;

        _objectPooler.GetFromPool(_particle, touchPos, Quaternion.identity);
    }
}