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
    void Start() {
        UiManagerGo uiManager = GameObject.Find("UiManager").GetComponent<UiManagerGo>();
        gameManager = uiManager.gameManager;
        gameManager.onGridChange += HandleGridChanged;
        gameManager.onUnitAdded += HandleUnitAdded;
        // grab prefabs
        unitPrefab = Resources.Load("Prefabs/Unit") as GameObject;
        gridPrefab = Resources.Load("Prefabs/Grid") as GameObject;
    }

    private void HandleUnitAdded(object sender, EventArgs args) {
        Unit unit = ((UnitAddedEventArgs) args).unit;
        MakeUnitGo(unit);
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
            Quaternion.identity
        ).GetComponent<UnitGo>();
        unitGo.unit = unit;
    }

    private void MakeGridGo() {
        gridGo = GameObject.Instantiate(
            gridPrefab,
            Vector3.zero,
            Quaternion.identity
        ).GetComponent<GridGo>();
    }
}