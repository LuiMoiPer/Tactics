using System;
using UnityEngine;

/*
 * Manages any additional linking of gameobjects or sctipts within and between scenes.
 */
public class SceneRunner : MonoBehaviour {
    GameManager gameManager;
    void Start() {
        UiManagerGo uiManagerGo = GameObject.Find("UiManager").GetComponent<UiManagerGo>();
        uiManagerGo.onGameManagerChanged += HandleGameManagerChanged;
        if (uiManagerGo.gameManager != null) {
            HandleGameManagerChanged(uiManagerGo, EventArgs.Empty);
        }
    }

    private void HandleGameManagerChanged(object sender, EventArgs args) {
        gameManager = ((UiManagerGo) sender).gameManager;
        gameManager.grid = new Grid(8, 8);
        gameManager.AddUnit(new Unit(new Coord(2, 2)));
        gameManager.AddUnit(new Unit(new Coord(3, 7)));
    }
}