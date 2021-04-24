using Priority_Queue;
using System;
using System.Collections.Generic;
/// <summary>
/// Takes care of high level game actions such as keeping track of unit turns and sending selected 
/// actions to unit.
/// </summary>
public class GameManager {
    public event EventHandler onSelectedUnitChanged;
    public event EventHandler onGridChanged;
    public event EventHandler onUnitAdded;

    public Grid grid {
        get {
            return _grid;
        }
        set {
            onGridChanged?.Invoke(this, EventArgs.Empty);
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
        onUnitAdded?.Invoke(this, new UnitAddedEventArgs(unit));
    }

    public void Select(Unit unit) {
        if (unit != _selectedUnit) {
            _selectedUnit = unit;
            onSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
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