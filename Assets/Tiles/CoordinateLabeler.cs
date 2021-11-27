using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro label;
    [SerializeField] Color DefaultColor = Color.white;
    [SerializeField] Color BlockedColor  = Color.grey;
    private Vector2Int coordinates = new Vector2Int();
    private Waypoint Waypoint;




    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        Waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        ColorCoordinates();
        ToggleCoordinates();
    }

    private void ToggleCoordinates()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            label.enabled = !label.enabled;
        }
    }

    private void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void ColorCoordinates()
    {
        label.color = Waypoint.IsPlacable ? DefaultColor : BlockedColor;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
