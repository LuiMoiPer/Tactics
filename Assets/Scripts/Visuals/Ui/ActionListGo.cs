using UnityEngine;
using TMPro;
public class ActionListGo : MonoBehaviour {
    private int horizontalSpacing = 1;
    private int verticalSpacing = 1;
    private GameObject buttonPrefab;

    void Start() {
        GetPrefabs();
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void ShowActions(ActionType[] actionTypes) {
        ClearActions();

        RectTransform rectTransform;
        GameObject button;
        float yPosition = -verticalSpacing;

        foreach (ActionType actionType in actionTypes) {
            rectTransform = MakeButton(actionType);
            rectTransform.anchoredPosition = new Vector2(horizontalSpacing, yPosition);
            yPosition -= rectTransform.rect.height + verticalSpacing;
        }
    }

    public void ClearActions() {
        foreach (Transform button in gameObject.transform) {
            Destroy(button.gameObject);
        }
    }

    private RectTransform MakeButton(ActionType actionType) {
        GameObject button = GameObject.Instantiate(
            buttonPrefab,
            gameObject.transform.position,
            Quaternion.identity,
            gameObject.transform
        );
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(0f, 1f);
        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.pivot = new Vector2(0f, 1f);

        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.SetText(actionType.ToString());

        return rectTransform;
    }

    private void GetPrefabs() {
        buttonPrefab = Resources.Load<GameObject>("Prefabs/Button");
    }
}