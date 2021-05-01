using UnityEngine;

public class UnitGo : MonoBehaviour {
    public Unit unit {
        get {
            return _unit;
        }
        set {
            SetUnit(value);
        }
    }
    private Unit _unit;

    private void SetUnit(Unit unit) {
        this._unit = unit;
        Debug.Log("Unit set:" + unit);
        UpdateVisual();
    }
 
    void Update() {
        if (unit != null) {
            UpdateVisual();
        }
    }

    private void UpdateVisual() {
        gameObject.transform.position = new Vector3(unit.position.x, 0f, unit.position.y);
    }
}