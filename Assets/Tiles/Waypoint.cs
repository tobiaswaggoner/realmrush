using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlacable = false;

    public bool IsPlacable 
    { 
        get
        {
            return isPlacable;
        } 
    }
    [SerializeField] Tower Tower; 
    private void OnMouseEnter() 
    {
        if(!isPlacable) return;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    private void OnMouseExit() 
    {
        if(!isPlacable) return;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnMouseDown() 
    {
        if(!isPlacable) return;

        var placed = Tower.CreateTower(transform.position);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        isPlacable = !placed;
        
    }

}
