using UnityEngine;

public class UI_GameObjectToggle : UI_PlayerPrefsToggle
{
    [Header("References")]
    [SerializeField] private GameObject _fpsGameObject;
    
    protected override void  OnValueChanged(bool state)
    {
        _fpsGameObject.SetActive(state);
        
        base.OnValueChanged(state);
    }
}
