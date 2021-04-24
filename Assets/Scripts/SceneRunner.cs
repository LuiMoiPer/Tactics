using UnityEngine;

/*
 * Manages any additional linking of gameobjects or sctipts within and between scenes.
 */
public class SceneRunner : MonoBehaviour {

    GridGo gridGo;
    // Prefabs
    GameObject unitPrefab;
    GameManager gameManager;
    UnitGo unitGo;
    void Start() {
        GameObject go = GameObject.Find("Grid");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SetGrid(new Grid(8, 8));
        gridGo = go.GetComponent<GridGo>();
        gridGo.setGrid(gameManager.GetGrid());
        unitPrefab = Resources.Load("Prefabs/Unit") as GameObject;
        MakeUnit(new Coord(2, 2));
        MakeUnit(new Coord(4, 7));
    }

    private void MakeUnit(Coord position) {
        unitGo = GameObject.Instantiate(
            unitPrefab,
            Vector3.zero,
            Quaternion.identity
        ).GetComponent<UnitGo>();
        Unit unit = new Unit(position);
        unit.IncreaseCooldown(position.x);
        unitGo.SetUnit(unit);
        gameManager.AddUnit(unit);
    }
}