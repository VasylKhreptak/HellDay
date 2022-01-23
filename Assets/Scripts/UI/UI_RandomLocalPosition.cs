using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UI_RandomLocalPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectTransform;

    [Header("Preferences")]
    [SerializeField] private Vector3[] _localPositions;

    private void OnEnable()
    {
        SetRandomPosition();
    }

    private void SetRandomPosition()
    {
        _rectTransform.localPosition = _localPositions.Random();
    }

    #region EDITOR

#if UNITY_EDITOR

    [CustomEditor(typeof(UI_RandomLocalPosition))]
    public class UI_RandomLocalPositionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UI_RandomLocalPosition targetScript = (UI_RandomLocalPosition)target;

            if (targetScript == null)
                return;

            if (GUILayout.Button("Save Local Position"))
            {
                Array.Resize(ref targetScript._localPositions, targetScript._localPositions.Length + 1);

                targetScript._localPositions[targetScript._localPositions.Length - 1] =
                    targetScript._rectTransform.localPosition;
            }
        }
    }

#endif

    #endregion
}