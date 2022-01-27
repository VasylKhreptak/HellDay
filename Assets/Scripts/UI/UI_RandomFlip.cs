using UnityEngine;

public class UI_RandomFlip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private bool _x = true;
    [SerializeField] private bool _y = true;

    private void OnEnable()
    {
        FlipRandomly();
    }

    private void FlipRandomly()
    {
        if (_x)
        {
            transform.localScale = new Vector3(Extensions.Random.Sign() * transform.localScale.x, 
                transform.localScale.y, 
                transform.localScale.z);
        }

        if (_y)
        {
            transform.localScale = new Vector3(transform.localScale.x, 
                Extensions.Random.Sign() * transform.localScale.y, 
                transform.localScale.z);
        }
    }
}