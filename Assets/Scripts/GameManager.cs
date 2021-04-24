using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Takes care of high level game actions such as keeping track of unit turns and sending selected 
/// actions to unit.
/// </summary>
public class GameManager : MonoBehaviour {
    private SimplePriorityQueue<Unit> unitsByCooldown = new SimplePriorityQueue<Unit>();
    private Unit selectedUnit = null;
    private GameObject selectionPrefab;
    private GameObject selectionVisual;
    private UiManager uiManager;
    private Grid grid;

    RaycastHit hit;
    Ray ray;
    void Start() {
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        selectionPrefab = Resources.Load("Prefabs/Selection") as GameObject;
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
            Debug.Log("Played");
        }
        UpdateSelection();
    }

    public void AddUnit(Unit unit) {
        unitsByCooldown.Enqueue(unit, unit.cooldown);
        Debug.Log(unitsByCooldown.Count);
        Debug.Log(unitsByCooldown.First.cooldown);
    }

    private void HandleClick() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        UnitGo unitGo = null;
        selectedUnit = null;
        if (Physics.Raycast(ray, out hit)
            && hit.transform != null
            && (bool) (unitGo = hit.transform.GetComponent<UnitGo>())
        ) {
            selectedUnit = unitGo.GetUnit();
            Debug.Log(selectedUnit);
        }
    }

    private void UpdateSelection() {
        if (selectedUnit != null) {
            selectionVisual.SetActive(true);
            selectionVisual.transform.position = new Vector3(selectedUnit.position.x, 0f, selectedUnit.position.y);
            uiManager.Highlight(ValidPositions(selectedUnit.neighboors));
        }
        else {
            selectionVisual.SetActive(false);
        }
    }

    public void PlayTurn() {
        Unit unit = unitsByCooldown.First;
        unit.IncreaseCooldown(unit.position.y);
        unitsByCooldown.UpdatePriority(unit, unit.cooldown);
        selectedUnit = unitsByCooldown.First;
    }

    public List<Coord> ValidPositions(Coord[] positions) {
        List<Coord> validPositions = new List<Coord>();
        foreach (Coord position in positions) {
            if (grid.isValidPosition(position)) {
                validPositions.Add(position);
            }
        }
        return validPositions;
    }
    
    public void SetGrid(Grid grid) {
        this.grid = grid;
    }

    public Grid GetGrid() {
        return grid;
    }
}