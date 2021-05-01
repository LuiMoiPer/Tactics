using Priority_Queue;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Takes care of high level game actions such as keeping track of unit turns and sending selected 
/// actions to unit.
/// </summary>
public class GameManagerGo : MonoBehaviour {
    private GameManager gameManager;

    private GameObject unitPrefab;
    private GameObject gridPrefab;
    private GridGo gridGo;
    Transform unitParent;
    void Start() {
        unitParent = new GameObject("UnitParent").transform;
        unitParent.SetParent(gameObject.transform);
        GetPrefabs();
        UiManagerGo uiManager = GameObject.Find("UiManager").GetComponent<UiManagerGo>();
        uiManager.onGameManagerChanged += HandleGameManagerChanged;
        // fire event handlers if gm is not null
        if (uiManager.gameManager != null) {
            HandleGameManagerChanged(uiManager, EventArgs.Empty);
            HandleGridChanged(null, EventArgs.Empty);
        }
    }

    private void GetPrefabs() {
        unitPrefab = Resources.Load<GameObject>("Prefabs/Unit");
        gridPrefab = Resources.Load<GameObject>("Prefabs/Grid");
    }

    private void HandleUnitAdded(object sender, EventArgs args) {
        Unit unit = ((UnitAddedEventArgs) args).unit;
        MakeUnitGo(unit);
    }

    private void HandleGameManagerChanged(object sender, EventArgs args) {
        gameManager = ((UiManagerGo) sender).gameManager;
        gameManager.onGridChanged += HandleGridChanged;
        gameManager.onUnitAdded += HandleUnitAdded;
        ClearUnitGos();
        foreach (Unit unit in gameManager.units) {
            MakeUnitGo(unit);
        }
    }

    private void HandleGridChanged(object sender, EventArgs args) {
        if (gridGo == null) {
            MakeGridGo();
        }
        gridGo.grid = gameManager.grid;
    }

    private void MakeUnitGo(Unit unit) {
        UnitGo unitGo = GameObject.Instantiate(
            unitPrefab,
            Vector3.zero,
            Quaternion.identity,
            unitParent
        ).GetComponent<UnitGo>();
        unitGo.unit = unit;
    }

    private void ClearUnitGos() {
        foreach (Transform child in unitParent) {
            Destroy(child.gameObject);
        }
    }

    private void MakeGridGo() {
        gridGo = GameObject.Instantiate(
            gridPrefab,
            Vector3.zero,
            Quaternion.identity
        ).GetComponent<GridGo>();
    }
}