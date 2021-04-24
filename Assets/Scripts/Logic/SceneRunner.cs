using UnityEngine;

/*
 * Manages any additional linking of gameobjects or sctipts within and between scenes.
 */
public class SceneRunner : MonoBehaviour {
    GameManager gameManager;
    void Start() {
        UiManagerGo uiManagerGo = GameObject.Find("UiManager").GetComponent<UiManagerGo>();
        gameManager = uiManagerGo.gameManager;
        gameManager.grid = new Grid(8, 8);
    }
}