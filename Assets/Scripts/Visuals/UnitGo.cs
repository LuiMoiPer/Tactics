using UnityEngine;

public class UnitGo : MonoBehaviour {
    Unit unit;
    [SerializeField]
    int cooldown = 0;

    public void SetUnit(Unit unit) {
        this.unit = unit;
        Debug.Log("Unit set:" + unit);
        UpdateVisual();
    }

    public Unit GetUnit() {
        return unit;
    }
 
    void Update() {
        if (unit != null) {
            cooldown = unit.cooldown;
        }
    }

    private void UpdateVisual() {
        gameObject.transform.position = new Vector3(unit.position.x, 0f, unit.position.y);
    }
}