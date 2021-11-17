using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KillableTargetDetection))]
public class KillableTargetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        KillableTargetDetection killableTargetDetection =  (KillableTargetDetection)target;

        if (GUILayout.Button("Find killable targets"))
        {
            killableTargetDetection.FindKillableTargets();
        }
    }
}
