using System;
using UnityEngine;

public class EnvironmentCheckerCore : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EnvironmentCheckerCoreData _data;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_data.layerMask.ContainsLayer(other.gameObject.layer)) OnEnterSmth();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnExitSmth();
    }

    protected virtual void OnEnterSmth()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnExitSmth()
    {
        throw new NotImplementedException();
    }
}
