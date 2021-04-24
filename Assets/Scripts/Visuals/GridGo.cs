
using UnityEngine;

public class GridGo : MonoBehaviour {
    private Grid grid;

    [SerializeField]
    GameObject tilePrefab;

    public void setGrid(Grid grid) {
        this.grid = grid;
        destroyChildren();
        makeGridVisual();
    }
    private void makeGridVisual() {
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

    private void destroyChildren() {
        foreach (Transform child in gameObject.transform) {
            DestroyImmediate(child.gameObject);
        }
    }
}
