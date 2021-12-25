using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    public void OnDestroy()
    {
        if (gameObject.scene.isLoaded == false) return;

        Destroy(_transform.parent.gameObject);
    }
}
