using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all the menus for the game, from start to action selection.
/// </summary>
public class UiManager : MonoBehaviour {
    GameManager gameManager;
    GameObject highlightPrefab;
    Transform highlightParent;
    void Start() {
        highlightParent = new GameObject("HighlightParent").transform;
        highlightParent.SetParent(gameObject.transform);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        highlightPrefab = Resources.Load("Prefabs/Highlight") as GameObject;
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
}