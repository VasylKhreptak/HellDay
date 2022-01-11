using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DamageableTargetDetection))]
public class KillableTargetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        var damageableTargetDetection = (DamageableTargetDetection)target;

        if (GUILayout.Button("Find killable targets")) damageableTargetDetection.FindKillableTargets();
    }
}