using UnityEngine;

public class ActionListGo : MonoBehaviour {
    private GameObject buttonPrefab;

    void Start() {
        GetPrefabs();
    }

    public void ShowActions(ActionType[] actionTypes) {

    }

    public void HideActions() {

    }

    private void MakeButton(ActionType actionType) {

    }

    private void GetPrefabs() {
        buttonPrefab = Resources.Load<GameObject>("Prefabs/Button");
    }
}