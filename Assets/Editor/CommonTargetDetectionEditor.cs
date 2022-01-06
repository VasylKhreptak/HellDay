using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CommonTargetDetection))]
public class CommonTargetDetectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        var commonTargetDetection = (CommonTargetDetection)target;

        if (GUILayout.Button("Find targets")) commonTargetDetection.FindTargets();
    }
}
