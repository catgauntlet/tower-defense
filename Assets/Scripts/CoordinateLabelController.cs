using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CoordinateLabelController : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    WaypointController waypoint;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
        label.enabled = false;

        waypoint = GetComponentInParent<WaypointController>();
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinates.x},{coordinates.y}";

        UpdateObjectName();
        UpdateLabelStyle();
    }

    void UpdateLabelStyle()
    {
        if (waypoint.IsBuildableTile)
        {
            label.color = defaultColor;
        } else
        {
            label.color = blockedColor;
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
