using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelCompleteObserver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _zombies;

    public static Action onLevelComplete;

    private int _killedZombies;
    
    private void OnEnable()
    {
        Zombie.onDeath += CheckZombies;
    }

    private void OnDisable()
    {
        Zombie.onDeath -= CheckZombies;
    }

    private void CheckZombies()
    {
        _killedZombies++;

        if (_killedZombies == _zombies.Length)
        {
            onLevelComplete?.Invoke();
        }
    }
    
    #region  EDITOR
    
    [CustomEditor(typeof(LevelCompleteObserver))]
    public class LevelCompleteObserverEditor : Editor
    {
        private LevelCompleteObserver targetScript;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            targetScript = (LevelCompleteObserver)target;

            if (targetScript == null)
                return;

            EditorGUILayout.Space();

            if (GUILayout.Button("Find zombies"))
            {
                AssignZombies();
            }
        }

        private void AssignZombies()
        {
            targetScript._zombies = FindObjectsOfType<Zombie>().Select(x => x.gameObject).ToArray();
        }
    }
    #endregion
}