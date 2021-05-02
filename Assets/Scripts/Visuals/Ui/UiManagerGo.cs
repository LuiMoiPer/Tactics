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

    private ActionListGo actionList;
    private GameObject highlightPrefab;
    private GameObject selectionPrefab;
    private GameObject selectionVisual;
    private GameObject canvasGo;
    private Transform highlightParent;
    private RaycastHit hit;
    private Ray ray;
    private GameManager _gameManager;
    void Start() {
        GetPrefabs();
        canvasGo = transform.Find("Canvas").gameObject;
        // make game objects
        actionList = GameObject.Instantiate(
            Resources.Load<GameObject>("Prefabs/ActionList"),
            new Vector3(0, 0, 0),
            Quaternion.identity,
            canvasGo.transform
        ).GetComponent<ActionListGo>();

        highlightParent = new GameObject("HighlightParent").transform;
        highlightParent.SetParent(gameObject.transform);

        selectionVisual = GameObject.Instantiate(
            selectionPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity,
            gameObject.transform
        );
        selectionVisual.SetActive(false);

        // setup non unity stuff
        _gameManager = new GameManager();
        _gameManager.onSelectedUnitChanged += HandleSelectedUnitChange;
        onGameManagerChanged?.Invoke(this, EventArgs.Empty);
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

    private void GetPrefabs() {
        highlightPrefab = Resources.Load<GameObject>("Prefabs/Highlight");
        selectionPrefab = Resources.Load<GameObject>("Prefabs/Selection");
    }

    private void HandleSelectedUnitChange(object sender, EventArgs args) {
        ClearHighlight();
        Unit selectedUnit = gameManager.selectedUnit;
        if (selectedUnit != null) {
            selectionVisual.SetActive(true);
            selectionVisual.transform.position = new Vector3(
                selectedUnit.position.x,
                0f,
                selectedUnit.position.y
            );
            actionList.ShowActions(selectedUnit.possibleActions);
        }
        else {
            selectionVisual.SetActive(false);
            actionList.ClearActions();
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
        else {
            gameManager.Select(null);
        }
    }
}