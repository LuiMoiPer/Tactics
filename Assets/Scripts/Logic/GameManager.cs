using Priority_Queue;
using System.Collections.Generic;
/// <summary>
/// Takes care of high level game actions such as keeping track of unit turns and sending selected 
/// actions to unit.
/// </summary>
public class GameManager {
    public event EventHandler onSelectedUnitChange;

    public Grid grid {
        get {
            return _grid;
        }
        set {
            _grid = value;
        }
    }

    public Unit selectedUnit {
        get {
            return _selectedUnit;
        }
    }

    private SimplePriorityQueue<Unit> unitsByCooldown;
    private Unit _selectedUnit;
    private Grid _grid;

    public GameManager() {
        this._grid = null;
        this._selectedUnit = null;
        this.unitsByCooldown = new SimplePriorityQueue<Unit>();
    }

    public void AddUnit(Unit unit) {
        unitsByCooldown.Enqueue(unit, unit.cooldown);
        Debug.Log(unitsByCooldown.Count);
        Debug.Log(unitsByCooldown.First.cooldown);
    }

    private void Select(Unit unit) {
        if (unit != _selectedUnit) {
            _selectedUnit = unit;
            onSelectedUnitChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayTurn() {
        Unit unit = unitsByCooldown.First;
        unit.IncreaseCooldown(unit.position.y);
        unitsByCooldown.UpdatePriority(unit, unit.cooldown);
        Select(unitsByCooldown.First);
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
}