using UnityEngine;
using UnityEngine.EventSystems;

public class OnPonterDownRandomForce : MonoBehaviour, IPointerDownHandler
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Data")]
    [SerializeField] private OnPonterDownRandomForceData _data;


    public void OnPointerDown(PointerEventData eventData)
    {
        _rigidbody.AddForceAtPosition(
            Extensions.Vector2.RandomDirection1() * Random.Range(_data.MinForce, _data.MaxForce),
            eventData.pointerCurrentRaycast.worldPosition, _data.forceMode2D);
        _rigidbody.AddTorque(Random.Range(_data.MinTorque, _data.MaxTorque));
    }
}