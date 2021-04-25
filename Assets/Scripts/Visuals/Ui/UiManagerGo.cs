using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all the menus for the game, from start to action selection.
/// </summary>
public class UiManagerGo : MonoBehaviour {
    public event EventHandler onGameManagerChanged;

    public GameManager gameManager {
        get {
            return _gameManager;
        }
    }
    GameObject highlightPrefab;
    private GameObject selectionPrefab;
    private GameObject selectionVisual;
    Transform highlightParent;
    RaycastHit hit;
    Ray ray;

    private GameManager _gameManager;
    void Start() {
        // setup non unity stuff
        _gameManager = new GameManager();
        _gameManager.onSelectedUnitChanged += HandleSelectedUnitChange;
        onGameManagerChanged?.Invoke(this, EventArgs.Empty);
        // grab prefabs
        highlightPrefab = Resources.Load("Prefabs/Highlight") as GameObject;
        selectionPrefab = Resources.Load("Prefabs/Selection") as GameObject;
        // make game objects
        highlightParent = new GameObject("HighlightParent").transform;
        highlightParent.SetParent(gameObject.transform);

        selectionVisual = GameObject.Instantiate(
            selectionPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity,
            gameObject.transform
        );
        selectionVisual.SetActive(false);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            HandleClick();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayTurn();
        }
    }

    public void PlayTurn() {
        gameManager.PlayTurn();
    }

    public void Highlight(List<Coord> positions) {
        ClearHighlight();
        foreach (Coord position in positions) {
            GameObject.Instantiate(
                highlightPrefab,
                new Vector3(position.x, 0, position.y),
                Quaternion.identity,
                highlightParent
            );
        }
    }

    private void ClearHighlight() {
        foreach (Transform child in highlightParent) {
            Destroy(child.gameObject);
        }
    }

    private void HandleSelectedUnitChange(object sender, EventArgs args) {
        ClearHighlight();
        if (gameManager.selectedUnit != null) {
            Highlight(gameManager.ValidPositions(gameManager.selectedUnit.neighboors));
        }
    }

    private void HandleClick() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        UnitGo unitGo = null;
        if (Physics.Raycast(ray, out hit)
            && hit.transform != null
            && (bool) (unitGo = hit.transform.GetComponent<UnitGo>())
        ) {
            gameManager.Select(unitGo.unit);
        }
    }
}