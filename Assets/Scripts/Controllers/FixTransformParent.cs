using UnityEngine;

public class FixTransformParent : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform _transform;

    private Transform _previousParent;

    private void OnEnable()
    {
      //  _previousParent = _transform.parent;
        
        //Debug.Log("Previous parent name : " + (_previousParent.name));
    }

    private void OnDisable()
    {
        //if (gameObject.scene.isLoaded == false) return;

        //_transform.parent = _previousParent;
    }
}
