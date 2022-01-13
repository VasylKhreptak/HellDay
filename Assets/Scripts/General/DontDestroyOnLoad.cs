using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        var objs = FindObjectsOfType<DontDestroyOnLoad>();

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
