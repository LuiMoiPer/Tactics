using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrefabHolder))]
public class PrefabHolderEditor : Editor {
    private bool showDictionary;
    private string status = "Test";
    private PrefabHolder prefabHolder;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (prefabHolder == null) {
            prefabHolder = (PrefabHolder) target;
        }
        else {
            HandleDictionary();
            EntryRow();
        }
    }

    private void HandleDictionary() {
        showDictionary = EditorGUILayout.BeginFoldoutHeaderGroup(showDictionary, status);
        if (showDictionary) {
            foreach (KeyValuePair<string, GameObject> kv in prefabHolder.prefabs) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.TextField(kv.Key);
                EditorGUILayout.ObjectField(kv.Value, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    private void EntryRow() {

    }
}