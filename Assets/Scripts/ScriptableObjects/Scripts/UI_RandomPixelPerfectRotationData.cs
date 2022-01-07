using UnityEngine;

[CreateAssetMenu(fileName = "UI_RandomPixelPerfectRotationData",
    menuName = "ScriptableObjects/UI_RandomPixelPerfectRotationData")]
public class UI_RandomPixelPerfectRotationData:ScriptableObject
{
    [HideInInspector] public float[] possibleZAngles = new[] { 0f, 90, 180f, 270f };
}
