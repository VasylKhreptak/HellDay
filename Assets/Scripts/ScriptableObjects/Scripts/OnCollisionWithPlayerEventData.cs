using UnityEngine;

[CreateAssetMenu(fileName = "OnCollisionWithPlayerEventData", 
 menuName = "ScriptableObjects/OnCollisionWithPlayerEventData")]
public class OnCollisionWithPlayerEventData : ScriptableObject
{ 
 [Header("Preferences")]
 [SerializeField] private float _interactDelay = 0.2f;

 public LayerMask playerLayerMask;

 public float InteractDelay => _interactDelay;
}
