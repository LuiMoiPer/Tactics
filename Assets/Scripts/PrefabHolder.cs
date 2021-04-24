using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour {
    [SerializeField]
    public Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>() {
        {"Test", null}
    };

    public GameObject GetPrefab(string name) {
        if (prefabs.ContainsKey(name)) {
            return prefabs[name];
        }
        else {
            return null;
        }
    }
}
