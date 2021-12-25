using UnityEngine;

public class BridgeElement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2D;
    public PlatformEffector2D platformEffector2D;

    [Header("Linked bridge elements")]
    [SerializeField] private BridgeElement[] _nextBridgeElements;

    [HideInInspector] public bool isDeactivated;

    private void OnDestroy()
    {
        foreach (var bridge in _nextBridgeElements)
        {
            if (bridge == null) continue;

            DeactivateBridgeElement(bridge);
        }
    }

    public void DeactivateBridgeElement(BridgeElement bridgeElement)
    {
        if (bridgeElement.isDeactivated) return;

        bridgeElement.boxCollider2D.usedByEffector = false;
        Destroy(bridgeElement.platformEffector2D);

        bridgeElement.rb.constraints = RigidbodyConstraints2D.None;

        bridgeElement.isDeactivated = true;
    }
}
