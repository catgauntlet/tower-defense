using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CoordinateLabelController : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.blue;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.yellow;

    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        label = GetComponent<TMP_Text>();
        label.enabled = true;

        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            // Only run in edit mode
            DisplayCoordinates();
        }

        UpdateLabelStyle();
        ToggleLabels();
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null)
        {
            return;
        }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = $"{coordinates.x},{coordinates.y}";
        UpdateObjectName();
        UpdateLabelStyle();
    }

    void UpdateLabelStyle()
    {
        if (gridManager == null) {
            return;
        }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) {
            label.color = defaultColor;
            return;
        }

        if (!node.isWalkable) {
            label.color = blockedColor;
        } else if (node.isPath) {
            label.color = pathColor;
        } else if (node.isExplored) {
            label.color = exploredColor;
        } else {
            label.color = defaultColor;
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
}
