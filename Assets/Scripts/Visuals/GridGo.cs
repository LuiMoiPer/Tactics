
using UnityEngine;

public class GridGo : MonoBehaviour {
    public Grid grid {
        get {
            return _grid;
        }
        set {
            SetGrid(value);
        }
    }
    
    private GameObject tilePrefab;
    
    private Grid _grid;

    public void SetGrid(Grid grid) {
        this.grid = grid;
        DestroyChildren();
        if (grid != null) {
            MakeGridVisual();
        }
    }
    private void MakeGridVisual() {
        for (int i = 0; i < grid.width; i++) {
            for (int j = 0; j < grid.height; j++) {
                // make tile
                GameObject tile = GameObject.Instantiate(
                    tilePrefab,
                    new Vector3(i, 0, j),
                    Quaternion.identity,
                    gameObject.transform
                );
                tile.name = i + ", " + j;
            }
        }
    }

    private void DestroyChildren() {
        foreach (Transform child in gameObject.transform) {
            DestroyImmediate(child.gameObject);
        }
    }
}
